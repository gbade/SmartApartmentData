using System.Collections.Generic;
using SmartApartmentData.Entities.Models;
using Newtonsoft.Json;

namespace SmartApartmentData.Web.Models
{
    public class PropertyViewModel
    {
        public Properties Property { get; set; }
        public List<PropertyData> Data { get; set; }

        public PropertyViewModel()
        {
            Data = new List<PropertyData>();
        }
    }

    public class PropertyData
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
        public float Latitude { get; set; }
        [JsonProperty("lng")]
        public float Longitude { get; set; }
        [JsonProperty("mgmtID")]
        public int MgmtID { get; set; }
    }
}
