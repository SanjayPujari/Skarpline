using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkarplineChat.App.SignalRHub
{
 /// <summary>
 /// To Display connected or active users.
 /// </summary>
    public  class ConnectedUser
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string ConnectionDateTime { get; set; }
     
    }
}
