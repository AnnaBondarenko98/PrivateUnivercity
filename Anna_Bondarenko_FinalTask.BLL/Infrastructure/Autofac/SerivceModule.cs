using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.DAL.EF;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Repositories;
using Autofac;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Infrastructure.Autofac
{
    public class ServiceModule : Module
    {
        private readonly string _connection;

        public ServiceModule(string connection)
        {
            _connection = connection;
        }

        protected override void Load(ContainerBuilder builder)
        {   
            builder.RegisterType<CommitteeContext>().As<IСommitteeContext>().WithParameter("connection", _connection).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<MessageSender.MessageSender>().As<IMessageSender>().SingleInstance();

            builder.Register(logger=>LogManager.GetLogger("*")).As<ILogger>();
        }
    }
}
