using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMPL46.Startup))]
namespace SMPL46
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
