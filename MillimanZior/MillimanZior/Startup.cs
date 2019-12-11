using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MillimanZior.Startup))]
namespace MillimanZior
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
