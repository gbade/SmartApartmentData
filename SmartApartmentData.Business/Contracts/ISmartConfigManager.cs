using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Business.Contracts
{
    public interface ISmartConfigManager
    {
        public string APIGateway { get; set; }
        public string AuthKey { get; set; }
    }
}
