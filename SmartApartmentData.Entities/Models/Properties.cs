using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text;

namespace SmartApartmentData.Entities.Models
{
    public class Properties
    {
        [JsonProperty("propertyID")]
        public int PropertyID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("formerName")]
        public string FormerName { get; set; }
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("market")]
        public string Market { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("lat")]
        public float Lat { get; set; }
        [JsonProperty("lng")]
        public float Lng { get; set; }
        [JsonProperty("mgmtID")]
        public int MgmtID { get; set; }
    }
}
