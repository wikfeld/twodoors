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

            container.RegisterType<IDoorAccessControl, DoorAccessControl>();
            container.RegisterInstance<IDoorRepository>(new TwoDoorsRepository(), new ContainerControlledLifetimeManager());
            container.RegisterInstance<IDoorAccessTokenRepository>(new StaticDataFactory().CreateTokenRepository(), new ContainerControlledLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}