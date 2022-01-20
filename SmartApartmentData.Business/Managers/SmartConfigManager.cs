using System;
using System.Collections.Generic;
using SmartApartmentData.Business.Contracts;
using System.Text;

namespace SmartApartmentData.Business.Managers
{
    public class SmartConfigManager : ISmartConfigManager
    {
        public string APIGateway { get; set; }
        public string AuthKey { get; set; }
    }
}
