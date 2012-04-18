﻿[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof($rootnamespace$.App_Start.NinjectMVC3), "Stop")]

namespace $rootnamespace$.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
	using Ninject.Web.Mvc;

    public static class NinjectMVC3 
	{
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
		{
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
			bootstrapper.Initialize(CreateKernel);
        }
		
        /// <summary>
        /// Stops the application.
        /// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}
		
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void RegisterServices(IKernel kernel)
        {
        }		
    }
}
