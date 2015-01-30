using RestSharp;
using SkarplineChat.App.Helper;
using SkarplineChat.Model.Model;
using SkarplineChat.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Uncomment when demo with client to show the Task #2
            //DeserializeJSONString();
            return View();
        }

        
        /// <summary>
        /// Calls when user logged in into the system. Calls the web api using RestSharp. 
        /// On successful login user is redirected to chat page
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            var username = Request.Form["username"];
            var action="home/login/";
            var result = RestHelper.ExecuteGetRequest<UserViewModel>(action, username);
            ViewBag.UserName = result.UserName;
            Session["User"] = result;
            return RedirectToAction("Chat");
        }

        /// <summary>
        /// Calls when user logged out/ disconnected from the system. 
        /// Calls the web api  using RestSharp to update database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Logout()
        {
            var username = ((UserViewModel)Session["User"]).UserName;
            var action = "home/logout/";
            var result = RestHelper.ExecuteGetRequest<UserViewModel>(action, username);
            ViewBag.UserName = "";
            return RedirectToAction("Index");
        }
               
        /// <summary>
        /// Displays the top 15 chats of various users
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult Chat()
        {
            if (Session["User"] == null)
            {
                return View("Index");
            }
            var action = "home/GetMessages/";
            var result = RestHelper.ExecuteGetRequest<List<UserChatViewModel>>(action);
            ViewBag.UserName = ((UserViewModel)Session["User"]).UserName;
            ViewBag.Messages = result;
            return View();
        }

        private void DeserializeJSONString()
        {
            RestSharp.RestResponse response = new RestSharp.RestResponse();
            response.Content = "{\"has_title\":true,\"title\":\"GoodLuck\",\"entries\":[[\"/getting started.pdf\",{\"thumb_exists\":false,\"path\":\"/Getting Started.pdf\",\"client_mtime\":\"Wed, 08 Jan 2014 18:00:54 +0000\",\"bytes\":249159}],[\"/task.jpg\",{\"thumb_exists\":true,\"path\":\"/Ta sk.jpg\",\"client_mtime\":\"Tue, 14 Jan 2014 05:53:57 +0000\",\"bytes\":207696}]]}";

            RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();
            Store objFromJson = deserializer.Deserialize<Store>(response);

            bool has_title = objFromJson.has_title;
            string title = objFromJson.title;
            List<List<object>> entries = objFromJson.entries;

            foreach (List<object> item in entries)
            {
                for (int i = 0; i < item.Count(); i++)
                {
                    Type t = item[i].GetType();
                    if (t == typeof(Dictionary<string, object>))
                    {
                        Dictionary<string, object> entry = (Dictionary<string, object>)item[i];
                        foreach (var obj in entry)
                        {
                            string key = obj.Key;
                            string value = obj.Value.ToString();
                        }
                    }
                    else
                    {
                        string fileName = item[i].ToString();
                    }
                }
            }
        }

    }
}