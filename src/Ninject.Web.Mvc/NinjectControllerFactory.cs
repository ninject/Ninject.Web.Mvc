using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninject.Web.Mvc
{
	/// <summary>
	/// A controller factory that creates <see cref="IController"/>s via Ninject.
	/// </summary>
	public class NinjectControllerFactory : DefaultControllerFactory
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
		/// Gets a controller instance of type controllerType.
		/// </summary>
		/// <param name="controllerType">Type of controller to create.</param>
		/// <returns>The controller instance.</returns>
		protected override IController GetControllerInstance( Type controllerType )
		{
			var controller = Kernel.TryGet( controllerType ) as IController;

			if( controller == null )
				return base.GetControllerInstance( controllerType );

			var standardController = controller as Controller;

			if( standardController != null )
				standardController.ActionInvoker = new NinjectActionInvoker( Kernel );

			return controller;
		}
	}
}