#region Using Directives

using System.Web.Http;
using Microsoft.Owin;
using Owin;
using WebApplication;

#endregion

[assembly: OwinStartup(typeof(Startup))]

namespace WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}