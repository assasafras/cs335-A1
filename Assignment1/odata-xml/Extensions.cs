using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace odata_xml
{
    public static class Extensions
    {


        public static DateTime? ParseDateTime(this XElement input)
        {
            DateTime? output = null;
            try
            {
                output = DateTime.Parse((string)input);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to Parse \"{input}\" to DateTime, returning null!");
            }
            return output;
        }

        public static int? ParseInt(this XElement input)
        {
            // Set the default as null.
            int? output = null;
            try
            {
                // Try parse to an integer.
                output = int.Parse((string)input);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to Parse \"{input}\" to int, returning null!");
            }
            return output;
        }
    }
}