using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_sql.Startup))]
namespace mvc_sql
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
