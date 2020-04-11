

$(document).ready(function () {

  $(".e1").click(function (event) {
    var client = $('.chat.active-chat').attr('client');

    var prevMsg = $('#chatFrom .chatboxtextarea').val();
    var shortname = $(this).data('shortname');

    $('#chatFrom .chatboxtextarea').val(prevMsg + ' ' + shortname + ' ');
    //$('#chatFrom .chatboxtextarea').focus();
  });
  $(".chat-head .personName").click(function () {
    var personName = $(this).text();
  });





  $(".header-close").click(function () {
    $('#wchat .wchat').removeClass('three');
    $('#wchat .wchat').addClass('two');
    $('.wchat-three').css({ 'display': 'none' });

  });

  $(".scroll-down").click(function () {
    scrollDown();
  });

  $("#mute-sound").click(function () {
    if (eval(localStorage.sound)) {
      localStorage.sound = false;
      $("#mute-sound").html('<i class="icon icon-volume-off"></i>');
    }
    else {
      localStorage.sound = true;
      $("#mute-sound").html('<i class="icon icon-volume-2"></i>');
      audiomp3.play();
      audioogg.play();
    }
  });
  $("#MobileChromeplaysound").click(function () {
    if (eval(localStorage.sound)) {
      audiomp3.play();
      audioogg.play();
    }
  });
  if (eval(localStorage.sound)) {
    $("#mute-sound").html('<i class="icon icon-volume-2"></i>');
  }
  else {
    $("#mute-sound").html('<i class="icon icon-volume-off"></i>');
  }

  //For Mobile on keyboard show/hide

  /*var _originalSize = $(window).width() + $(window).height()
  $(window).resize(function(){
      if($(window).width() + $(window).height() != _originalSize){
          //alert("keyboard show up");
          $(".target-emoji").css({'display':'none'});
          $('.wchat-filler').css({'height':0+'px'});

      }else{
          //alert("keyboard closed");
          $('#chatFrom .chatboxtextarea').blur();
      }
  });*/
});


function chatemoji() {
  //$(".target-emoji").slideToggle( 'fast', function(){

  //    if ($(".target-emoji").css('display') == 'block') {
  //        //alert($(window).height());
  //        //$('.chat-list').css({'height':(($(window).height())-279)+'px'});
  //        $('.wchat-filler').css({'height':225+'px'});
  //        $('.btn-emoji').removeClass('ti-face-smile').addClass('ti-arrow-circle-down');
  //    } else {
  //        //$('.chat-list').css({'height':(($(window).height())-179)+'px'});
  //        $('.wchat-filler').css({'height':0+'px'});
  //        $('.btn-emoji').removeClass('ti-arrow-circle-down').addClass('ti-face-smile');
  //    }
  //});
  //var heit = $('#resultchat').css('max-height');
}

function typePlace() {

  if (!$('#textarea').html() == '') {
    $(".input-placeholder").css({ 'visibility': 'hidden' });
  }
  else {
    $(".input-placeholder").css({ 'visibility': 'visible' });
  }

}



//Inbox User search
$(document).ready(function () {
  $('.contact-list li').each(function () {
    $(this).attr('data-search-term', $(this).text().toLowerCase());
  });

  $('.live-search-box').on('keyup', function () {
    var searchTerm = $(this).val().toLowerCase();
    $('.live-search-list li').each(function () {

      if ($(this).filter('[data-search-term *= ' + searchTerm + ']').length > 0 || searchTerm.length < 1) {
        $(this).show();
      } else {
        $(this).hide();
      }
    });
  });
});

$(window).bind("load", function () {
  $('.person:first').trigger('click');
  var personName = $('.person:first').find('.personName').text();
  $('.right .top .personName').html(personName);
  //$('.right .top').attr("data-user",personName);
  var userImage = $('.person:first').find('.userimage').html();
  $('.right .top .userimage').html(userImage);
  var personStatus = $('.person:first').find('.personStatus').html();
  $('.right .top .personStatus').html(personStatus);
  var hideContent = $('.person:first').find('.hidecontent').html();
  $('.right .hidecontent').html(hideContent);

  /*$('[contenteditable]').on('paste',function(e) {
      e.preventDefault();
      var text = (e.originalEvent || e).clipboardData.getData('text/plain')
      document.execCommand('insertText', false, text);
  });
*/
  $('.chatboxtextarea').on('focus', function (e) {
    $(".target-emoji").css({ 'display': 'none' });
    $('.wchat-filler').css({ 'height': 0 + 'px' });
  });
});


$('#display').on('mousedown', '.person', function () {
  if ($(this).hasClass('.active')) {
    return false;
  } else {
    var findChat = $(this).attr('data-chat');
    var personName = $(this).find('.personName').text();
    $('.right .top .personName').html(personName);
    //$('.right .top').attr("data-user",personName);
    var userImage = $(this).find('.userimage').html();
    $('.right .top .userimage').html(userImage);
    var personStatus = $(this).find('.personStatus').html();
    $('.right .top .chat-status').html(personStatus);
    var hideContent = $(this).find('.hidecontent').html();
    $('.right .hidecontent').html(hideContent);
    $('.chat').removeClass('active-chat');
    $('.left .person').removeClass('active');
    $(this).addClass('active');
    var chatId = $(this).data('chat_id');
    _.forEach(chatList, e => {
      if (e.chatuser == chatId) {
        e.active = true;
        e.count = 0;
      } else {
        e.active = false;
      }
    });
    $(`#chatbox1_${chatId} .count span`).attr('class', "").html(null);
    $('.chat[data-chat = ' + findChat + ']').addClass('active-chat');
  }
});

var chatList = [];

function setChatList(chatuser, toid, img, status, userName) {
  var chat = null;
  var statsChange = 0;
  for (var i = 0; i < chatList.length; i++) {
    var thisChat = chatList[i];
    if (thisChat.chatuser == chatuser) {
      chat = thisChat;
      break;
    }
  }
  if (chat == null) {
    chatList.push({
      chatuser: chatuser,
      toid: toid,
      img: img,
      status: status,
      userName: userName,
      count: 0,
      active: false,
    });
    statsChange++;
  } else {
    if (!chat.active) {
      chat.count = chat.count + 1;
    }
    chat.userName = userName;
    if (chat.status != status) {
      chat.status = status
      statsChange++;
    }
  }
  if (statsChange) {
    $('#display').html(null);
  }
  var online = _.filter(chatList, b => b.status == 'Online');
  var offline = _.filter(chatList, b => b.status != 'Online');
  _.forEach(online, c => {
    createList(c.chatuser, c.toid, c.img, c.status, c.userName, c.active, c.count);
  });
  _.forEach(offline, c => {
    createList(c.chatuser, c.toid, c.img, c.status, c.userName, c.active, c.count);
  });
}

function createList(chatuser, toid, img, status, userName, active, count) {
  var testObj = $(`#chatbox1_${chatuser}`);
  if (testObj.length > 0) {
    $(`#chatbox1_${chatuser} .personStatus span`).attr('class', status);
    $(`#chatbox1_${chatuser} .preview span`).attr('class', status).html(status);
    if (!$(`#chatbox1_${chatuser}`).hasClass('active') && count) {
      $(`#chatbox1_${chatuser} .count span`).attr('class', 'icon-meta unread-count').html(count);
    }
    console.log(userName)
    $(`#chatbox1_${chatuser} .bname.personName`).html(userName);
    return;
  }
  $('#display').append(`
<li class="person chatboxhead" id="chatbox1_${chatuser}" data-chat_id="${chatuser}" data-chat="person_${toid}" href="javascript:void(0)" onclick="javascript:chatWith('${chatuser}','${toid}','${img}','${status}')">
    <a href="javascript:void(0)">
        <span>
            <span class="bname personName">${userName}</span>
            <span class="personStatus"><span class="${status}"></span></span>
            <span class="count"><span class=""></span></span><br>
            <small class="preview"><span class="${status}">${status}</span></small>
        </span>
    </a>
</li>
`);
}
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
$(document).ready(function () {
  connection.start().then(function () {
    connection.invoke("StaffConnect").catch(function (err) {
      return console.error(err.toString());
    });
  });
  connection.on("ReceiveMessage", function (user, message) {
    setChatList(user, user, '', message.offline ? 'Offline' : 'Online', message.userName);
    createChatBox(user, user, '', message.offline ? 'Offline' : 'Online');

    var msg = message.message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    $(`#chatbox_${user}`).append(`
      <div class="col-xs-12 p-b-10 ${ message.odd ? 'odd' : ''}">
        <div class="chat-body">
          <div class="chat-text">
            <h4>${message.userName}</h4>
            <p>${msg}</p>
          </div>
        </div>
      </div>`);
    scrollDown();
  });
})



