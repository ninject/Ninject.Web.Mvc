// 
// Authors: Nate Kohari <nate@enkari.com>, Josh Close <narshe@gmail.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Web.Mvc
{
    using System;
    using System.Web.Mvc;

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
        /// <param name="controllerType">Type of controller to create.</param>
        /// <returns>The controller instance.</returns>
        protected override IController GetControllerInstance(Type controllerType)
        {
            var controller = this.Kernel.Get(controllerType) as IController;

            if (controller == null)
            {
                return base.GetControllerInstance(controllerType);
            }

            var standardController = controller as Controller;

            if (standardController != null)
            {
                standardController.ActionInvoker = this.CreateActionInvoker();
            }

            return controller;
        }

        /// <summary>
        /// Creates the action invoker.
        /// </summary>
        /// <returns>The action invoker.</returns>
        protected virtual NinjectActionInvoker CreateActionInvoker()
        {
            return new NinjectActionInvoker(this.Kernel);        
        }
    }
}