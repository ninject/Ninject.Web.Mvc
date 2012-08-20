//-------------------------------------------------------------------------------
// <copyright file="NinjectMvcHttpApplicationPlugin.cs" company="bbv Software Services AG">
//   Copyright (c) 2011 bbv Software Services AG
//   Author: Remo Gloor (remo.gloor@gmail.com)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Ninject.Web.Mvc
{
    using System.Web;
    using System.Web.Mvc;

	using Ninject.Activation;
    using Ninject.Components;
    using Ninject.Web.Common;

    /// <summary>
    /// The web plugin implementation for MVC
    /// </summary>
    public class NinjectMvcHttpApplicationPlugin : NinjectComponent, INinjectHttpApplicationPlugin
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectMvcHttpApplicationPlugin"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectMvcHttpApplicationPlugin(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Gets the request scope.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The request scope.</returns>
        public object GetRequestScope(IContext context)
        {
            return HttpContext.Current;
        }
        
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            ControllerBuilder.Current.SetControllerFactory(this.CreateControllerFactory());
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Creates the controller factory that is used to create the controllers.
        /// </summary>
        /// <returns>The created controller factory.</returns>
        protected virtual NinjectControllerFactory CreateControllerFactory()
        {
            return new NinjectControllerFactory(this.kernel);
        }
    }
}