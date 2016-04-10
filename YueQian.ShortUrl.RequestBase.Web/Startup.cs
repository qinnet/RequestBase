using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YueQian.ShortUrl.RequestBase.Web.Startup))]
namespace YueQian.ShortUrl.RequestBase.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
