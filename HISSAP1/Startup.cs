using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HISSAP1.Startup))]
namespace HISSAP1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
