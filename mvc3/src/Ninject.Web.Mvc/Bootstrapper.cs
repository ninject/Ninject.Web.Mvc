//-------------------------------------------------------------------------------
// <copyright file="NinjectDependencyResolver.cs" company="bbv Software Services AG">
//   Copyright (c) 2010 bbv Software Services AG
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
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ninject.Syntax;
    using Ninject.Web.Mvc.Filter;
    using Ninject.Web.Mvc.Validation;

    /// <summary>
    /// A basic bootstrapper that can be used to setup web applications.
    /// </summary>
    public class Bootstrapper: IBootstrapper
    {
        /// <summary>
        /// The ninject kernel of the application
        /// </summary>
        protected static IKernel kernelInstance;

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        [Obsolete("Do not use Ninject as Service Locator")]
        public IKernel Kernel
        {
            get { return kernelInstance; }
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        public void Initialize(Func<IKernel> createKernelCallback)
        {
            if (kernelInstance != null)
            {
                throw new InvalidOperationException("Already Initialized!");
            }

            kernelInstance = createKernelCallback();

            kernelInstance.Bind<IResolutionRoot>().ToConstant(kernelInstance);
            kernelInstance.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();
            kernelInstance.Bind<IFilterProvider>().To<NinjectFilterAttributeFilterProvider>();
            kernelInstance.Bind<IFilterProvider>().To<NinjectFilterProvider>();
            kernelInstance.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
            kernelInstance.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
            kernelInstance.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
            kernelInstance.Bind<ModelValidatorProvider>().To<NinjectDataAnnotationsModelValidatorProvider>();

            ModelValidatorProviders.Providers.Remove(ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().Single());
            DependencyResolver.SetResolver(CreateDependencyResolver());
            RemoveDefaultAttributeFilterProvider();
        }

        /// <summary>
        /// Initializes a HttpApplication instance.
        /// </summary>
        /// <param name="httpApplication">The HttpApplication instance.</param>
        public void InitializeHttpApplication(HttpApplication httpApplication)
        {
            kernelInstance.Inject(httpApplication);
        }

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        public void ShutDown()
        {
            if (kernelInstance != null)
            {
                kernelInstance.Dispose();
                kernelInstance = null;
            }
        }

        /// <summary>
        /// Creates the controller factory that is used to create the controllers.
        /// </summary>
        /// <returns>The created controller factory.</returns>
        private static IDependencyResolver CreateDependencyResolver()
        {
            return kernelInstance.Get<IDependencyResolver>();
        }

        /// <summary>
        /// Removes the default attribute filter provider.
        /// </summary>
        private static void RemoveDefaultAttributeFilterProvider()
        {
            var oldFilter = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldFilter);
        }
    }
}