using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KLMPortfolio.Startup))]
namespace KLMPortfolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
