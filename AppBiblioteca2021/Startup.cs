using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppBiblioteca2021.Startup))]
namespace AppBiblioteca2021
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
