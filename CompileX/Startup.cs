using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompileX.Startup))]
namespace CompileX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
