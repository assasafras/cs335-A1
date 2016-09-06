using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace odata_xml.Data
{
    public class XMLDataSource
    {
        private static IEnumerable<Customer> customers;
        public IQueryable<Customer> Customers
        {
            get { return customers.AsQueryable(); }
        }

        private static IEnumerable<Employee> employees;
        public IQueryable<Employee> Employees
        {
            get { return employees.AsQueryable(); }
        }

        private static IEnumerable<Order> orders;
        public IQueryable<Order> Orders
        {
            get { return orders.AsQueryable(); }
        }

        private const string baseDir = @"C:\usertmp\";
        private const string customersXMLPath = baseDir + "XCustomers.xml";
        private const string ordersXMLPath = baseDir + "XOrders.xml";
        private const string employeesXMLPath = baseDir + "XEmployees.xml";

        static XMLDataSource()
        {
            // Create each entity from its XML document.
            
            // First the customers.
            BuildCustomers();

            // Then the Employees.
            BuildEmployees();

            // Finally Orders, also restoring FK Constraints.
            BuildOrders();
        }

        private static void BuildCustomers()
        {
            using (var reader = new StreamReader(customersXMLPath))//, Encoding.GetEncoding("iso-8859-1")))
            {

                var xmlString = reader.ReadToEnd();
                // Replace illegal characters within the XML.
                xmlString = xmlString.Replace("&", "&amp;");
                xmlString = xmlString.Replace("\"", "&quot;");
                xmlString = xmlString.Replace("'", "&apos;");

                customers = XElement.Parse(xmlString)
                .Elements("Customer")
                .Select
                (
                    c => new Customer
                    {
                        CustomerID = (string)c.Element("CustomerID")
                        , CompanyName = (string)c.Element("CompanyName")
                        , ContactName = (string)c.Element("ContactName")
                    }
                )
                .ToArray();
            }
        }

        private static void BuildEmployees()
        {
            using (var reader = new StreamReader(employeesXMLPath))//, Encoding.GetEncoding("iso-8859-1")))
            {
                string xmlString = reader.ReadToEnd();
                // Replace illegal characters within the XML.
                xmlString = xmlString.Replace("&", "&amp;");
                xmlString = xmlString.Replace("\"", "&quot;");
                xmlString = xmlString.Replace("'", "&apos;");
                employees = XElement.Parse(xmlString)
                        .Elements("Employee")
                        .Select
                        (
                            e => new Employee
                            {
                                EmployeeID = (int)e.Element("EmployeeID")
                                , FirstName = (string)e.Element("FirstName")
                                , LastName = (string)e.Element("LastName")
                            }
                        )
                        .ToArray(); 
            }
        }

        private static void BuildOrders()
        {
            using (var reader = new StreamReader(ordersXMLPath))//, Encoding.GetEncoding("iso-8859-1")))
            {
                try
                {
                    var xmlString = reader.ReadToEnd();
                    // Replace illegal characters within the XML.
                    xmlString = xmlString.Replace("&", "&amp;");
                    xmlString = xmlString.Replace("\"", "&quot;");
                    xmlString = xmlString.Replace("'", "&apos;");

                    // Push order elements into an enumerable to make query syntax a bit more readable.
                    var orderElements = XElement.Parse(xmlString)
                                            .Elements("Order");
                    // inner join on customers and left join on employees.
                    orders = (from o in orderElements
                              join customer in customers on o.Element("CustomerID").Value equals customer.CustomerID
                              join employee in employees on o.Element("EmployeeID").ParseInt() equals employee.EmployeeID into gj
                              from e in gj.DefaultIfEmpty()
                              select new Order
                              {
                                  OrderID = (int)o.Element("OrderID"),
                                  Freight = (decimal)o.Element("Freight"),
                                  ShipCity = (string)o.Element("ShipCity"),
                                  OrderDate = (DateTime)o.Element("OrderDate"),
                                  ShipCountry = (string)o.Element("ShipCountry"),
                                  ShipName = (string)o.Element("ShipName"),
                                  ShippedDate = o.Element("ShippedDate").ParseDateTime(),
                                  CustomerID = (string)o.Element("CustomerID"),
                                  EmployeeID = o.Element("EmployeeID").ParseInt(),
                                  Customer = customer,
                                  Employee = e
                              }).ToArray();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}