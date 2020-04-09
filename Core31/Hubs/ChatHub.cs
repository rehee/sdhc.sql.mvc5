using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Core31.Hubs
{
  public class ChatHub : Hub
  {
    public static Dictionary<string, HubUserMap> userMaper = new Dictionary<string, HubUserMap>();
    public static IReadOnlyList<string> StaffIds => userMaper.Where(b => b.Value.IsStaff).Select(b => b.Key).ToList() as IReadOnlyList<string>;
    public static List<MissingMessage> AllMissingMessage = new List<MissingMessage>();
    private string id => Context.ConnectionId;
    private string userID => Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    private HubUserMap CurrentHubUser
    {
      get
      {
        var userMap = userMaper.GetValueOrDefault(id);
        if (userMap == null)
        {
          userMap = newHubUserMap();
          userMaper.Add(id, userMap);
        }
        return userMap;

      }
    }
    private HubUserMap newHubUserMap()
    {
      var user = Context.User;
      var isAuthenticated = user.Identity.IsAuthenticated;
      var userName = isAuthenticated ? user.Identity.Name : null;
      return new HubUserMap(userName, false, isAuthenticated, userID, id);
    }
    public ChatHub()
    {

    }

    public async Task CustomConnect(string email)
    {
      await NoStaffOnline();
      if (String.IsNullOrEmpty(CurrentHubUser.Email))
      {
        CurrentHubUser.Email = email;
      }
      await ToAllStaff(CurrentHubUser.ClientId, new ChatMessage(CurrentHubUser.ClientId, null, CurrentHubUser.Email, "customer online"));
    }
    public async Task MessageToMe(string message, string messageFrom = "Me")
    {
      if (String.IsNullOrEmpty(CurrentHubUser.UserId))
      {
        await Clients.Caller.SendAsync("ReceiveMessageFromMe", id, new ChatMessage(id, id, messageFrom, message));
      }
      else
      {
        await Clients.User(CurrentHubUser.UserId).SendAsync("ReceiveMessageFromMe", id, new ChatMessage(id, id, messageFrom, message));
      }

    }
    public async Task SendMessageToStaff(string message)
    {
      await MessageToMe(message);
      await NoStaffOnline(message);
      await ToAllStaff(CurrentHubUser.ClientId, new ChatMessage(CurrentHubUser.ClientId, null, CurrentHubUser.Email, message));
    }
    public async Task StaffConnect()
    {
      CurrentHubUser.IsStaff = true;
      var allMissing = AllMissingMessage.Select(b => b).ToList();
      var userMessages = allMissing.Where(b => !String.IsNullOrEmpty(b.UserId)).GroupBy(b => b.UserId).ToList();
      var allLiveConnection = userMaper.Values.Where(b => !b.IsStaff).GroupBy(b => b.UserId);
      var allLiveUser = allLiveConnection.Where(b => !String.IsNullOrEmpty(b.Key)).Select(b => b.FirstOrDefault()).ToList();

      foreach (var userMessage in userMessages)
      {
        foreach (var message in userMessage)
        {
          if (String.IsNullOrEmpty(message.Message))
          {
            continue;
          }
          await Clients.Caller.SendAsync("ReceiveMessage", message.UserId, new ChatMessage(message.UserId, null, message.UserName, message.Message, true));
        }
      }
      foreach (var user in allLiveUser)
      {
        await Clients.Caller.SendAsync("ReceiveMessage", user.ClientId, new ChatMessage(user.UserId, null, user.Email, "customer connected"));
      }

      var messages = allMissing.Where(b => String.IsNullOrEmpty(b.UserId)).GroupBy(b => b.ConnectId).ToList();
      foreach (var msgGroup in messages)
      {
        foreach (var message in msgGroup)
        {
          if (String.IsNullOrEmpty(message.Message))
          {
            continue;
          }
          await Clients.Caller.SendAsync("ReceiveMessage", message.ConnectId, new ChatMessage(message.ConnectId, null, message.UserName, message.Message, true));
        }
      }
      var allMessager = allLiveConnection.Where(b => String.IsNullOrEmpty(b.Key)).SelectMany(b => b).ToList();
      foreach (var user in allMessager)
      {
        await Clients.Caller.SendAsync("ReceiveMessage", user.ConnectId, new ChatMessage(user.ConnectId, null, user.Email, " customer connected"));
      }
      try
      {
        AllMissingMessage.Clear();
      }
      catch { }
    }
    public async Task SendMessageToCustom(string toId, string message)
    {
      if (String.IsNullOrEmpty(toId))
      {
        return;
      }
      var senderName = String.IsNullOrEmpty(CurrentHubUser.Email) ? "System" : CurrentHubUser.Email;
      var user = userMaper.Values.FirstOrDefault(b => b.UserId == toId);
      if (user != null)
      {
        await Clients.User(user.UserId).SendAsync("ReceiveMessage", toId, new ChatMessage(id, user.UserId, senderName, message));
      }
      else
      {
        await Clients.Client(toId).SendAsync("ReceiveMessage", toId, new ChatMessage(id, toId, senderName, message));
      }
      await Clients.Clients(StaffIds).SendAsync("ReceiveMessage", toId, new ChatMessage(id, toId, senderName, message, false, true));
    }


    public async Task NoStaffOnline(string message = null)
    {
      if (StaffIds.Count <= 0)
      {
        AllMissingMessage.Add(new MissingMessage(CurrentHubUser, id, message));
        await MessageToMe("No Customer Service Online now.", "System");
      }

    }
    public async Task ToAllStaff(string id, ChatMessage message)
    {
      await Clients.Clients(StaffIds).SendAsync("ReceiveMessage", id, message);
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
      var id = Context.ConnectionId;

      await Clients.Clients(StaffIds).SendAsync("ReceiveMessage", CurrentHubUser.ClientId, new ChatMessage(CurrentHubUser.ClientId, id, "", "Off Line", true));
      try
      {
        if (userMaper.ContainsKey(id))
        {
          userMaper.Remove(id);
        }
        userMaper.Keys.Where(b => String.IsNullOrEmpty(b)).ToList().ForEach(k => userMaper.Remove(k));
      }
      catch { }

      await base.OnDisconnectedAsync(exception);
    }


  }

  public class MissingMessage
  {
    public MissingMessage(HubUserMap user, string connectId, string message)
    {
      UserName = user.Email;
      ConnectId = connectId;
      UserId = user.UserId;
      Message = message;
      SendTime = DateTime.UtcNow;
    }
    public string UserName { get; }
    public string ConnectId { get; }
    public string UserId { get; }
    public DateTime SendTime { get; }
    public string Message { get; }

  }

  public class ChatMessage
  {
    public ChatMessage(string fromId, string toId, string userName, string message, bool offline = false, bool odd = false)
    {
      FromId = fromId;
      ToId = toId;
      UserName = userName;
      Message = message;
      Offline = offline;
      Odd = odd;
    }
    public string FromId { get; set; }
    public string ToId { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    public bool Offline { get; set; }
    public bool Odd { get; set; }
  }

  public class HubUserMap
  {
    public HubUserMap(string email, bool staff, bool isAuthenticated = false, string userId = null, string connectId = null)
    {
      this.Email = email;
      this.IsStaff = staff;
      this.IsAuthenticated = isAuthenticated;
      this.UserId = userId;
      this.ConnectId = connectId;
    }
    public string Email { get; set; }
    public bool IsStaff { get; set; }
    public bool IsAuthenticated { get; set; }
    public string UserId { get; set; }
    public string ConnectId { get; set; }

    public string ClientId => String.IsNullOrEmpty(UserId) ? ConnectId : UserId;
  }
}
