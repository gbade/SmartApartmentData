using System;
using Newtonsoft.Json;

namespace SmartApartmentData.Upload.Models
{
    public class Management
    {
        [JsonProperty("mgmtID")]
        public int ManagementID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("market")]
        public string Market { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class Data
    {
        [JsonProperty("mgmt")]
        public Management Management { get; set; }
    }
}
