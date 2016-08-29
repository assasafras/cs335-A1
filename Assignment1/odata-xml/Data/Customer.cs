using System.Collections.Generic;
using System.Data.Services.Common;

namespace odata_xml.Data
{
    [DataServiceKey("CustomerID")]
    public class Customer
    {
        public string       CustomerID  { get; set; }
        public string       CompanyName { get; set; }
        public string       ContactName { get; set; }
        //public List<Order>  Orders      { get; set; }
    } 
}