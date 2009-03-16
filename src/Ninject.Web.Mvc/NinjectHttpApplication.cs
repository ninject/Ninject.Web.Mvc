using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Infrastructure;

namespace Ninject.Web.Mvc
{
	/// <summary>
	/// Defines an <see cref="HttpApplication"/> that is controlled by a Ninject <see cref="IKernel"/>.
	/// </summary>
	public abstract class NinjectHttpApplication : HttpApplication, IHaveKernel
	{
		private static IKernel _kernel;

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
				_kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InRequestScope();
				_kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InRequestScope();
				
				ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(_kernel));

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
		/// Called when the application is started.
		/// </summary>
		protected virtual void OnApplicationStarted() { }

		/// <summary>
		/// Called when the application is stopped.
		/// </summary>
		protected virtual void OnApplicationStopped() { }
	}
}