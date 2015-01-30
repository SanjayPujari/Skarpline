using SkarplineChat.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkarplineChat.Model.ViewModel
{
 
    /// <summary>
    /// Displays the user messages
    /// </summary>
    public class UserChatViewModel
    {
        public UserChatViewModel()
        { }

        [DataMember]
        public UserViewModel User { get; set; }
        [DataMember]
        public Guid ChatId { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public System.DateTime MessageDateTime { get; set; }
        [DataMember]
        public Guid MessageFrom { get; set; }
    }
}
