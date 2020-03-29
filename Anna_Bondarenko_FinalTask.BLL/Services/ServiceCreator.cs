using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IEnrolleeService CreateUserService()
        {
            return DependencyResolver.Current.GetService<IEnrolleeService>();
        }

        public IOperatorService CreateOperatorService()
        {
            return DependencyResolver.Current.GetService<IOperatorService>();
        }

        public void Dispose()
        {
        }
    }
}
