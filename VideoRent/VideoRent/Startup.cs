using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoRent.Startup))]
namespace VideoRent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
