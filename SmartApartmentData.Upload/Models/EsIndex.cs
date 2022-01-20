using System;
namespace SmartApartmentData.Upload.Models
{
    public class EsIndex
    {
        public string _index { get; set; }
        public string _id { get; set; }
    }

    public class IdxRoot
    {
        public EsIndex index { get; set; }
    }
}
