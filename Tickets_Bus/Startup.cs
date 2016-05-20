using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tickets_Bus.Startup))]
namespace Tickets_Bus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
