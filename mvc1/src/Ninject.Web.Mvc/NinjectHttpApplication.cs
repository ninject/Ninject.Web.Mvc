#region License
// 
// Authors: Nate Kohari <nate@enkari.com>, Josh Close <narshe@gmail.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 
#endregion
#region Using Directives
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Infrastructure;
#endregion

namespace Ninject.Web.Mvc
{
    /// <summary>
	/// Defines an <see cref="HttpApplication"/> that is controlled by a Ninject <see cref="IKernel"/>.
	/// </summary>
	public abstract class NinjectHttpApplication : HttpApplication, IHaveKernel
	{
        /// <summary>
        /// The one per request module to release request scope at the end of the request
        /// </summary>
        private readonly OnePerRequestModule onePerRequestModule;
        
        private static IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectHttpApplication"/> class.
        /// </summary>
        protected NinjectHttpApplication()
        {
            this.onePerRequestModule = new OnePerRequestModule();
            this.onePerRequestModule.Init(this);
        }
        
        /// <summary>
		/// Gets the kernel.
		/// </summary>
		public IKernel Kernel
		{
			get { return _kernel; }
		}
        
        /// <summary>
		/// Starts the application.
		/// </summary>
		public void Application_Start()
		{
			lock (this)
			{
				_kernel = CreateKernel();

				_kernel.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
                _kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
                _kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

                ControllerBuilder.Current.SetControllerFactory(CreateControllerFactory());

				_kernel.Inject(this);

                if (_kernel.Settings.Get("ReleaseScopeAtRequestEnd", true))
                {
                    OnePerRequestModule.StartManaging(_kernel);
                }
                
                OnApplicationStarted();
			}
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public void Application_Stop()
		{
			lock (this)
			{
				if (_kernel != null)
				{
					_kernel.Dispose();
					_kernel = null;
				}

				OnApplicationStopped();
			}
		}

		/// <summary>
		/// Registers all controllers in the assembly with the specified name.
		/// </summary>
		/// <param name="assemblyName">Name of the assembly to search for controllers.</param>
		public void RegisterAllControllersIn(string assemblyName)
		{
			RegisterAllControllersIn(Assembly.Load(assemblyName), GetControllerName);
		}

		/// <summary>
		/// Registers all controllers in the assembly with the specified name.
		/// </summary>
		/// <param name="assemblyName">Name of the assembly to search for controllers.</param>
		/// <param name="namingConvention">The naming convention to use for the controllers.</param>
		public void RegisterAllControllersIn(string assemblyName, Func<Type, string> namingConvention)
		{
			RegisterAllControllersIn(Assembly.Load(assemblyName), namingConvention);
		}

		/// <summary>
		/// Registers all controllers in the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly to search for controllers.</param>
		public void RegisterAllControllersIn(Assembly assembly)
		{
			RegisterAllControllersIn(assembly, GetControllerName);
		}

		/// <summary>
		/// Registers all controllers in the specified assembly.
		/// </summary>
		/// <param name="assembly">The assembly to search for controllers.</param>
		/// <param name="namingConvention">The naming convention to use for the controllers.</param>
		public void RegisterAllControllersIn(Assembly assembly, Func<Type, string> namingConvention)
		{
			foreach (Type type in assembly.GetExportedTypes().Where(IsController))
				_kernel.Bind<IController>().To(type).InTransientScope().Named(namingConvention(type));
		}

		private static bool IsController(Type type)
		{
			return typeof(IController).IsAssignableFrom(type) && type.IsPublic && !type.IsAbstract && !type.IsInterface;
		}

		private static string GetControllerName(Type type)
		{
			string name = type.Name.ToLowerInvariant();

			if (name.EndsWith("controller"))
				name = name.Substring(0, name.IndexOf("controller"));

			return name;
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		protected abstract IKernel CreateKernel();

		/// <summary>
		/// Creates the controller factory that is used to create the controllers.
		/// </summary>
		/// <returns>The created controller factory.</returns>
		protected virtual NinjectControllerFactory CreateControllerFactory()
		{
			return new NinjectControllerFactory(Kernel);
		}

		/// <summary>
		/// Called when the application is started.
		/// </summary>
		protected virtual void OnApplicationStarted() { }

		/// <summary>
		/// Called when the application is stopped.
		/// </summary>
		protected virtual void OnApplicationStopped() { }
    }
}