using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPMedAPI.Startup))]
namespace ASPMedAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
