using System.Data.Services.Common;

namespace odata_xml.Data
{
    [DataServiceKey("EmployeeID")]
    public class Employee
    {
        public int      EmployeeID  { get; set; }
        public string   FirstName   { get; set; }
        public string   LastName    { get; set; }
    }
}