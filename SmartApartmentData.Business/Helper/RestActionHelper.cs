using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Net;
using RestSharp;
using System.Threading.Tasks;

namespace SmartApartmentData.Business.Helper
{
    public class RestActionHelper
    {
        public R CallGetAction<R>(string url, string authkey = "")
        {
            var response = default(R);
            try
            {
                var restResponse = GetRequest(url, authkey);

                if (restResponse.Content != null && restResponse.Content.Length > 0)
                {
                    response = JsonConvert.DeserializeObject<R>(restResponse.Content);
                }

                //_fileHelper.ProcessApiLogs(
                //    url, "GET", restResponse.Content, ""
                //);
            }
            catch (Exception ex)
            {
                //_fileHelper.ProcessErrors(ex);
                throw new Exception(ex.Message);
            }

            return response;
        }

        private static IRestResponse GetRequest(string requestUrl, string authkey)
        {
            try
            {
                IRestResponse response = new RestResponse();
                var client = new RestClient(requestUrl);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 
                                                        | SecurityProtocolType.Tls11 
                                                        | SecurityProtocolType.Tls;

                var request = new RestRequest(Method.GET);

                if (!string.IsNullOrEmpty(authkey))
                    request.AddHeader("Authorization", authkey);

                request.AddHeader("content-type", "application/json");

                Task.Run(async () =>
                {
                    response = await client.ExecuteAsync(request);
                }).Wait();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
