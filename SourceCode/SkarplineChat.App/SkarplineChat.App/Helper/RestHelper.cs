using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SkarplineChat.App.Helper
{
    /// <summary>
    /// Generic helper class for RestSharp for CRUD operations
    /// </summary>
    public static class RestHelper
    {
        static string ServiceURL = ConfigurationManager.AppSettings["ServiceURL"].ToString();

        /// <summary>
        /// Execute POST request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static T ExecutePostRequest<T>(string action, object parameter) where T : new()
        {
            var client = new RestClient(ServiceURL);
            RestRequest request = null;
            request = new RestRequest(action, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(parameter);
            return client.Execute<T>(request).Data;
        }

        public static T ExecuteGetRequest<T>(string action, string parameter = "") where T : new()
        {
            var client = new RestClient(ServiceURL);
            var request = new RestRequest(action + parameter, Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<T>(request);
            return response.Data;

        }
        public static string ExecuteGetRequest(string action, string parameter = "")
        {
            var client = new RestClient(ServiceURL);
            var request = new RestRequest(action + parameter, Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            return response.Content;

        }
    }

    public class Store
    {
        public bool has_title { get; set; }
        public string title { get; set; }
        public List<List<object>> entries { get; set; }
    }
}