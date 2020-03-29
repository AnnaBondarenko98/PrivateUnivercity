using System;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IServiceCreator : IDisposable
    {
        IEnrolleeService CreateUserService();

        IOperatorService CreateOperatorService();
    }
}
