using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DA2_SistemaEscolar2016.Startup))]
namespace DA2_SistemaEscolar2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
