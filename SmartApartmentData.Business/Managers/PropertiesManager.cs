using SmartApartmentData.Business.Contracts;
using SmartApartmentData.Entities.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Business.Managers
{
    public class PropertiesManager : IPropertiesManager
    {
        private readonly ISmartConfigManager _configManager;

        public PropertiesManager(ISmartConfigManager configManager) 
        {
            _configManager = configManager;
        }

        public IEnumerable<Properties> GetDistinctProperties(string queryParam, string filter) 
        {
            var properties = new List<Properties>();
            //get uri parameters and build
            //make api call
            var distinctProp = GroupDistinctProperties(properties);
            return distinctProp;
        }

        private IEnumerable<Properties> GroupDistinctProperties(List<Properties> properties) 
        {
            var distinctProperties = properties.GroupBy(p => new
            {
                p.Name,
                p.MgmtID,
                p.PropertyID
            }).Select(x => x.First());

            return distinctProperties;
        }
    }
}
