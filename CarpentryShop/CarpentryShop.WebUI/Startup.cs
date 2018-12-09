using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarpentryShop.WebUI.Startup))]
namespace CarpentryShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
