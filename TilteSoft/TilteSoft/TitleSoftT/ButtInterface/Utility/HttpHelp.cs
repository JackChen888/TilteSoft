using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleSoftT.ButtInterface.Utility
{
    public class HttpHelp
    {
        public static string SendAll(Method method, string url, string headData, string encryptBody)
        {
            var postUrl = url;
            //string postUrl = "http://localhost:8088/api/test";
            RestClient client = new RestClient(postUrl);
            client.Timeout = 60000;
            RestRequest request = new RestRequest(method);
            request.AddHeader("head", headData);
            request.AddParameter("body", encryptBody);
            string result = client.Execute(request).Content;
            return result;
        }

        public static string SendGet(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "46937335-2a39-4f97-90fb-a8db16bd3d26");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            return response.Content;
        }


        public static string SendPost(string url, string ids)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "ed1fa06b-6ed9-4e80-9006-85ef4585f696");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "ids=" + ids, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }


    }
}
