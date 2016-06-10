﻿using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Website.Foundation.Core.Identity;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Persistence;
using Website.Foundation.Persistence.Repositories;
using Microsoft.AspNet.Identity.Owin;
using log4net;

namespace $safeprojectname$.Codes
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            
            kernel.Load(Assembly.GetExecutingAssembly());
            
            log4net.Config.XmlConfigurator.Configure();

            RegisterAssemblies(kernel);
            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterAssemblies(KernelBase kernel)
        {
            List<string> listAssembly = new List<string>()
            {
                "Website.Foundation.*",
                "$safeprojectname$.Codes.*"
            };
            kernel.Bind(x =>
            {
                x.FromAssembliesMatching(listAssembly) // Scans all assemblies
                 .SelectAllClasses() // Retrieve all non-abstract classes
                 .BindDefaultInterface(); // Binds the default interface to them;
            });
        }
        private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()).InRequestScope();
            kernel.Bind<ApplicationRoleManager>().ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>()).InRequestScope();
        }
        
    }
}