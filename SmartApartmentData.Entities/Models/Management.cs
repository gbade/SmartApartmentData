using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace SmartApartmentData.Entities.Models
{
    public class Management
    {
        [JsonProperty("mgmtID")]
        public int MgmtID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("market")]
        public string Market { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
