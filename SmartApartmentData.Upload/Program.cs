using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using SmartApartmentData.Business.Helper;
using SmartApartmentData.Upload.Models;

namespace SmartApartmentData.Upload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UploadPropertyData();
            UploadManagementData();

            Console.WriteLine("Task Complete!");
        }
       
        private static void UploadPropertyData()
        {
            var prop = new DataUtil<Root>();
            var json = prop.LoadJson("properties.json");
            prop.WriteJsonToFile(json, "property");
            prop.UploadData("bulk_property.json");
        }

        private static void UploadManagementData()
        {
            var management = new DataUtil<Data>();
            var mgmtJson = management.LoadJson("mgmt.json");
            management.WriteJsonToFile(mgmtJson, "mgmt");
            management.UploadData("bulk_mgmt.json");
        }       
    }

    public class DataUtil<T>
    {
        private string _gateway;
        private string _authkey;
        private string dir = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../..")
            );

        public DataUtil()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(dir).AddJsonFile("appsettings.json").Build();

            Configuration = builder;

            _gateway = Configuration.GetSection("AwsGateway")
                            .GetSection("APIGateway").Value;
            _authkey = Configuration.GetSection("AwsGateway")
                            .GetSection("AuthKey").Value;
        }

        public IConfigurationRoot Configuration { get; }

        public void WriteJsonToFile(List<T> json, string filename)
        {
            var idx = 0;
            var path = $"{dir}/bulk_{filename}.json";

            foreach (var data in json)
            {
                idx++;
                var indexObj = new EsIndex { _index = filename, _id = $"{idx}" };
                var obj = new IdxRoot { index = indexObj };

                var idxJson = JsonConvert.SerializeObject(obj);
                object dataObj = new object();

                if (data is Data)
                {
                    var mgmt = (Data)(object)data;
                    dataObj = mgmt.Management;
                }
                else if (data is Root)
                {
                    var prop = (Root)(object)data;
                    dataObj = prop.property;
                }                    

                var bulkData = JsonConvert.SerializeObject(dataObj);

                using (TextWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(idxJson);
                    tw.WriteLine(bulkData);
                    tw.Flush();
                }
            }
        }

        public void UploadData(string filename)
        {
            var api = new RestActionHelper();
            var cred = new StringHelper();

            var path = $"{dir}/bulk_{filename}.json";
            var payload = ReadJsonFile(path);

            var credentials = cred.DecryptCredentials(_authkey);
            var result = api.CallPostAction<object>(payload, _gateway, credentials);
        }

        public List<T> LoadJson(string filename)
        {
            var items = new List<T>();

            var json = ReadJsonFile(filename);
            items = JsonConvert.DeserializeObject<List<T>>(json);

            return items;
        }

        private string ReadJsonFile(string filename)
        {
            var json = string.Empty;
            using (StreamReader r = new StreamReader(filename))
            {
                json = r.ReadToEnd();
            }
            return json;
        }
    }
}
