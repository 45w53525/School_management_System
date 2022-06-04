using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School_management_System.Startup))]
namespace School_management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
