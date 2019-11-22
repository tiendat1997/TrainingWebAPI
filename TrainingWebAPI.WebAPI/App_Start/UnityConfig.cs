using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TrainingWebAPI.Entity;
using TrainingWebAPI.Repository;
using TrainingWebAPI.Service;
using TrainingWebAPI.WebAPI.Common;
using TrainingWebAPI.WebAPI.Filters;
using TrainingWebAPI.WebAPI.Logging;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Injection;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace TrainingWebAPI.WebAPI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer()
                 .AddNewExtension<Interception>();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            var serviceAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("TrainingWebAPI.Service")).ToArray();
            var repositoryAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("TrainingWebAPI.Repository")).ToArray();
            container.RegisterTypes(RegisterTypesScan.GetTypesWithCustomAttribute<BusinessLogicAttribute>(serviceAssemblies), WithMappings.FromMatchingInterface, WithName.Default, WithLifetime.Transient,
                getInjectionMembers: t => new InjectionMember[] {
                    new Interceptor<InterfaceInterceptor>(), //Interception technique
                    new InterceptionBehavior<BusinessLogicBehaviour>()
                });

            container.RegisterTypes(RegisterTypesScan.GetTypesWithCustomAttribute<DataAccessAttribute>(repositoryAssemblies), WithMappings.FromMatchingInterface, WithName.Default, WithLifetime.Transient,
              getInjectionMembers: t => new InjectionMember[] {
                    new Interceptor<InterfaceInterceptor>(), //Interception technique
                    new InterceptionBehavior<DataAccessBehaviour>()
              });

            container
                    .RegisterType<LogHandler>(new ContainerControlledLifetimeManager())
                    .RegisterType<DbContext, EFTrainingEntities>(new ContainerControlledLifetimeManager());            


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}