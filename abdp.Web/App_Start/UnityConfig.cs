using abdp.Service.IService;
using abdp.Service.Service;

using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace abdp.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IOlssModelVehicleService, OlssModelVehicleService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
