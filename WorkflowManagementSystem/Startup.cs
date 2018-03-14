using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkflowManagementSystem.Startup))]
namespace WorkflowManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
