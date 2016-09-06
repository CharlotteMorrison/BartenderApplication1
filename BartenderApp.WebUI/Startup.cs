using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BartenderApp.WebUI.Startup))]
namespace BartenderApp.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
