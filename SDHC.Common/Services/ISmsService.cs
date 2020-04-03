using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDHC.Common.Services
{
  public interface ISmsService
  {
    Task SendSmsAsync(string number, string message);
  }
  public class SmsService : ISmsService
  {
    public Task SendSmsAsync(string number, string message)
    {
      return Task.CompletedTask;
    }
  }
}
