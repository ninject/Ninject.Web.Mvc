// 
// Authors: Nate Kohari <nate@enkari.com>, Remo Gloor <remo.gloor@gmail.com>, Josh Close <narshe@gmail.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Web.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    
    /// <summary>
    /// A controller factory that creates <see cref="IController"/>s via Ninject.
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectControllerFactory"/> class.
        /// </summary>
        /// <param name="kernel">The kernel that should be used to create controllers.</param>
        public NinjectControllerFactory(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        /// <summary>
        /// Gets the kernel that will be used to create controllers.
        /// </summary>
        public IKernel Kernel { get; private set; }
     
        /// <summary>
        /// Gets a controller instance of type controllerType.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerType">Type of controller to create.</param>
        /// <returns>The controller instance.</returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                // let the base handle 404 errors with proper culture information
                return base.GetControllerInstance(requestContext, controllerType);
            }

            var controller = this.Kernel.TryGet(controllerType) as IController;

            return controller ?? base.GetControllerInstance(requestContext, controllerType);
        }
    }
}