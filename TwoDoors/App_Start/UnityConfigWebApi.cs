using Microsoft.Practices.Unity;
using System.Web.Http;
using TwoDoors.Models;
using TwoDoors.Services;
using Unity.WebApi;

namespace TwoDoors
{
    public static class UnityConfigWebApi
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ITimeProvider, SystemTime>();
            container.RegisterType<IDoorAccessControl, DoorAccessControl>();
            container.RegisterInstance<IDoorRepository>(new TwoDoorsRepository(), new ContainerControlledLifetimeManager());
            container.RegisterInstance(new StaticDataFactory().CreateTokenRepository(), new ContainerControlledLifetimeManager());
            container.RegisterType<IAccessLogRepository>(new ContainerControlledLifetimeManager(), 
                new InjectionFactory(c => new StaticAccessLogRepository(c.Resolve<ITimeProvider>())));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}