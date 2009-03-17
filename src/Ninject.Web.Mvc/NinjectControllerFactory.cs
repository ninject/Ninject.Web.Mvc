using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.Web.Mvc
{
	/// <summary>
	/// A controller factory that creates <see cref="IController"/>s via Ninject.
	/// </summary>
	public class NinjectControllerFactory : IControllerFactory
	{
		/// <summary>
		/// Gets the kernel that will be used to create controllers.
		/// </summary>
		public IKernel Kernel { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NinjectControllerFactory"/> class.
		/// </summary>
		/// <param name="kernel">The kernel that should be used to create controllers.</param>
		public NinjectControllerFactory(IKernel kernel)
		{
			Kernel = kernel;
		}

		/// <summary>
		/// Creates the controller with the specified name.
		/// </summary>
		/// <param name="requestContext">The request context.</param>
		/// <param name="controllerName">Name of the controller.</param>
		/// <returns>The created controller.</returns>
		public IController CreateController(RequestContext requestContext, string controllerName)
		{
			return Kernel.TryGet<IController>(controllerName.ToLowerInvariant());
		}

		/// <summary>
		/// Releases the specified controller.
		/// </summary>
		/// <param name="controller">The controller to release.</param>
		public void ReleaseController(IController controller) { }
	}
}