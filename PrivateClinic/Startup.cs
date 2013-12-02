using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrivateClinic.Startup))]
namespace PrivateClinic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
