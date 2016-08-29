
using odata_xml.Data;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel;

namespace odata_xml
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class XMLService : DataService<XMLDataSource>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }
    }
}