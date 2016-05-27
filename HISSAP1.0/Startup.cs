using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HISSAP1._0.Startup))]
namespace HISSAP1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
