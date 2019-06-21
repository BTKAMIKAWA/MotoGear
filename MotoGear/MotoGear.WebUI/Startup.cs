using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MotoGear.WebUI.Startup))]
namespace MotoGear.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
