using ContentManagementSystem.Local.Controllers;
using ContentManagementSystem.Local.CustomIdentity;
using ContentManagementSystem.Local.Extensions;
using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;
using static ContentManagementSystem.Local.Models.IdentityModel;

namespace ContentManagementSystem.Local.Models
{
    public static class IocExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }

        public static void BindInRequestScope<T1>(this IUnityContainer container) where  T1: class
        {
            container.RegisterType<T1>(new HierarchicalLifetimeManager());
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }
    public class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            //Bind the various domain model services and repositories that e.g. our controllers require         
            container.BindInRequestScope<ISPRepository, SPRepository>();
            container.RegisterType<UserController>(new InjectionConstructor());
            IndentityExtension.Init(container.Resolve<ISPRepository>());
            return container;
        }
    }
}