using SmartApartmentData.Entities.Models;
using System.Collections.Generic;

namespace SmartApartmentData.Business.Contracts
{
    public interface IPropertiesManager
    {
        public IEnumerable<Properties> GetDistinctProperties(string queryParam, string filter);
    }
}
