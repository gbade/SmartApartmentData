using SmartApartmentData.Business.Contracts;
using SmartApartmentData.Business.Helper;
using SmartApartmentData.Entities.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartApartmentData.Business.Managers
{
    public class PropertiesManager : IPropertiesManager
    {
        private readonly ISmartConfigManager _config;

        public PropertiesManager(ISmartConfigManager config) 
        {
            _config = config;
        }

        public IEnumerable<Properties> GetDistinctProperties(Properties queryparams) 
        {
            var auth = _config.AuthKey;
            var api = new RestActionHelper();
            var properties = new List<Properties>();
            var cred = new StringHelper();

            //get uri parameters and build
            var url = BuildApiUrl(queryparams);

            //make api call
            var authkey = cred.DecryptCredentials(auth);
            var response = api.CallGetAction<QueryResponse>(url, authkey);
            if (response != null)
            {
                var res = response.hits.hits;
                foreach (var item in res)
                {
                    var source = item._source;
                    var sourceObj = JsonConvert.SerializeObject(source);
                    var property = JsonConvert.DeserializeObject<Properties>(sourceObj);

                    properties.Add(property);
                }
            }

            var distinctProp = GroupDistinctProperties(properties);
            return distinctProp;
        }



        private string BuildApiUrl(Properties parameters)
        {
            var baseUrl = _config.APIGateway;

            var queryString = parameters.ToUrlQueryString();
            var url = $"{baseUrl}/_search?{queryString}&size=25";

            return url;
        }        

        private IEnumerable<Properties> GroupDistinctProperties(List<Properties> properties) 
        {
            var distinctProperties = properties.GroupBy(p => new
            {
                p.name,
                p.mgmtID,
                p.propertyID
            }).Select(x => x.First());

            return distinctProperties;
        }
    }
}
