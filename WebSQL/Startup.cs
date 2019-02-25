using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSQL.Startup))]
namespace WebSQL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
