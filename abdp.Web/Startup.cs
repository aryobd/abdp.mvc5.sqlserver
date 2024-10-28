using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(abdp.Web.Startup))]
namespace abdp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
