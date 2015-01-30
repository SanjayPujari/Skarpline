using SkarplineChat.Model.Model;
using SkarplineChat.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace SkarplineChat.Api.Controllers
{
    public class HomeController : Controller
    {

        SkarplineChatDBEntities dbContext = new SkarplineChatDBEntities();
        public ActionResult Index()
        {
            return View("index");
        }
        
        /// <summary>
        /// Call when a user logged in. Saves the user into the database.
        /// </summary>
        /// <param name="id">Accept username</param>
        /// <returns>Return user detail</returns>
        public JsonResult Login(string id)
        {
            var existingUser = dbContext.Users.Any(o => o.UserName == id) ?
                dbContext.Users.Where(o => o.UserName == id).FirstOrDefault() :
                null;

            if (existingUser == null)
            {
                return Json(InsertUser(id), JsonRequestBehavior.AllowGet);
            }
            else
            {
                existingUser.IsLogout = false;
                existingUser.LoginDateTime = DateTime.Now;
                dbContext.SaveChanges();

                return Json(new UserViewModel(existingUser), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Calls when user disconnected or logout from application.
        /// </summary>
        /// <param name="id">Accept username</param>
        /// <returns>Returns user detail</returns>
        public JsonResult Logout(string id)
        {
            var existingUser = dbContext.Users.Any(o => o.UserName == id) ?
                dbContext.Users.Where(o => o.UserName == id).FirstOrDefault() :
                null;

            existingUser.IsLogout = true;
            existingUser.LogoutDateTime = DateTime.Now;
            dbContext.SaveChanges();
            return Json(new UserViewModel(existingUser), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetMessages()
        {

            var messages = dbContext.UserChats.OrderByDescending(o => o.MessageDateTime)
                                       .Take(15).Select(o => new UserChatViewModel
                                       {
                                           ChatId = o.ChatId,
                                           Message = o.Message,
                                           User = new UserViewModel { UserId = o.User.UserId, UserName = o.User.UserName },
                                           MessageFrom = o.MessageFrom,
                                           MessageDateTime = o.MessageDateTime

                                       }).ToList();
            var temp = messages.OrderBy(o => o.MessageDateTime).ToList();
            var jtemp = Json(temp, JsonRequestBehavior.AllowGet);
            return Json(messages.OrderBy(o=>o.MessageDateTime).ToList(), JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// Calls when any user broadcast the  message.
        /// </summary>
        /// <param name="userchat">Accept the details of message</param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult SendMessage(UserChat userchat)
        {

            UserChat userChat = new UserChat
            {
                ChatId = Guid.NewGuid(),
                Message = userchat.Message,
                MessageDateTime = DateTime.Now,
                MessageFrom = dbContext.Users.Where(o => o.UserName == userchat.User.UserName).FirstOrDefault().UserId,
                User = dbContext.Users.Where(o => o.UserName == userchat.User.UserName).FirstOrDefault()
            };

            dbContext.UserChats.Add(userChat);
            dbContext.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Calls when a new user logged in into the system
        /// </summary>
        /// <param name="UserName">Accept username</param>
        /// <returns></returns>
        private UserViewModel InsertUser(string UserName)
        {
            User user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = UserName,
                LoginDateTime = DateTime.Now,
                IsLogout = false
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return new UserViewModel(user);

        }
    }
}
