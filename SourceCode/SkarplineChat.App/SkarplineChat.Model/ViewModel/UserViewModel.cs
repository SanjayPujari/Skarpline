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
    /// Display users information.
    /// </summary>
    public class UserViewModel
    {
        public UserViewModel()
        { }
        public UserViewModel(User user)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            LoginDateTime = user.LoginDateTime;
            LogoutDateTime = user.LogoutDateTime;
            IsLogout = user.IsLogout;
        }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public System.DateTime? LoginDateTime { get; set; }

        public System.DateTime? LogoutDateTime { get; set; }

        public bool IsLogout { get; set; }


    }
}
