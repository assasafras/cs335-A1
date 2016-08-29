using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odata_xml.Data
{
    [DataServiceKey ("OrderID")]
    public class Order
    {
        public int          OrderID         { get; set; }
        public string       CustomerID      { get; set; }
        public int?         EmployeeID      { get; set; }
        public DateTime     OrderDate        { get; set; }
        public DateTime?     ShippedDate      { get; set; }
        public decimal      Freight          { get; set; }
        public string       ShipName        { get; set; }
        public string       ShipCity        { get; set; }
        public string       ShipCountry     { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
