using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using RestSharp;
using SkarplineChat.Model.Model;
using System.Threading.Tasks;
using System.Configuration;
using SkarplineChat.App.Helper;
using SkarplineChat.Model.ViewModel;

namespace SkarplineChat.App.SignalRHub
{
    /// <summary>
    /// Enables users to make remote procedure calls from a server to connected clients and from clients to the server.
    /// </summary>
    public class SignalRChatHub : Hub
    {
        static List<ConnectedUser> _connectedUsers = new List<ConnectedUser>();

        /// <summary>
        /// Call by client when a new user get connected and 
        ///  add the user to the list of online/active users and inform the other clients.
        /// </summary>
        /// <param name="username">Accept the username of recently joined user.</param>
        public void onConnection(string username)
        {
            if (username != null)
            {
                if (!_connectedUsers.Any(o => o.UserName == username))
                    _connectedUsers.Add(new ConnectedUser { ConnectionId = Context.ConnectionId, UserName = username, ConnectionDateTime = DateTime.Now.ToShortTimeString() });
            }
            Clients.All.joinedRoom(_connectedUsers, username);
        }

        /// <summary>
        /// Fires when any client get disconnected and
        /// removes the user from the list of online/active users and inform the other clients.
        /// </summary>
        /// <param name="stop"></param>
        /// <returns>Call the client side function leftRoom to update the online users list</returns>
        public override Task OnDisconnected(bool stop)
        {
            var userTobeDisconnected = _connectedUsers.Where(o => o.ConnectionId == Context.ConnectionId).FirstOrDefault();
            _connectedUsers.Remove(userTobeDisconnected);
            if (userTobeDisconnected != null)
            {
                var action="home/Logout/";
                RestHelper.ExecuteGetRequest<UserViewModel>(action, userTobeDisconnected.UserName);
                return Clients.All.leftRoom(_connectedUsers, userTobeDisconnected.UserName);
            }

            return Clients.All.leftRoom(_connectedUsers, null);
        }
        /// <summary>
        /// Broadcast message to all the active/online clients.
        /// </summary>
        /// <param name="username">Accept username of user who is sending the message</param>
        /// <param name="message">Accept message to be send</param>
 
        public void Send(string username, string message)
        {
            Clients.All.addNewMessageToPage(username, message);
            UserChat userchat = new UserChat
            {
                Message = message,
                MessageFrom = Guid.NewGuid(),
                MessageDateTime = DateTime.Now,
                User = new User { UserName = username }
            };
            var action= "home/SendMessage/";
            RestHelper.ExecutePostRequest<UserChatViewModel>(action, userchat);
        }
       /// <summary>
        /// Fires when user start typing in the message box and also inform other clients that who is typing.
       /// </summary>
       /// <param name="username">Accept the username of user who is typing.</param>
        public void WhoIsTyping(string username)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SignalRChatHub>();
            context.Clients.All.sayWhoIsTyping(username);
        }
    }
}