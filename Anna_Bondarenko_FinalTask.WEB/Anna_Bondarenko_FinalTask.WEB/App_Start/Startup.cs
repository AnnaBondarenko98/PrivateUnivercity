using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Services;
using Anna_Bondarenko_FinalTask.WEB;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Anna_Bondarenko_FinalTask.WEB
{
    public class Startup
    {
        private readonly IServiceCreator _serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.CreatePerOwinContext(CreateOperatorService);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private IEnrolleeService CreateUserService()
        {
            return _serviceCreator.CreateUserService();
        }

        private IOperatorService CreateOperatorService()
        {
            return _serviceCreator.CreateOperatorService();
        }
    }
}