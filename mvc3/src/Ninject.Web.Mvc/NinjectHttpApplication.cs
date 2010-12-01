//-------------------------------------------------------------------------------
// <copyright file="NinjectHttpApplication.cs" company="bbv Software Services AG">
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
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ninject.Infrastructure;
    using Ninject.Syntax;
    using Ninject.Web.Mvc.Filter;
    using Ninject.Web.Mvc.Validation;

    /// <summary>
    /// Defines an <see cref="HttpApplication"/> that is controlled by a Ninject <see cref="IKernel"/>.
    /// </summary>
    public abstract class NinjectHttpApplication : HttpApplication, IHaveKernel
    {
        /// <summary>
        /// The one per request module to release request scope at the end of the request
        /// </summary>
        private readonly OnePerRequestModule onePerRequestModule;

        /// <summary>
        /// The ninject kernel of the application
        /// </summary>
        private static IKernel kernel;

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
            get { return kernel; }
        }
        
        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Application_Start()
        {
            lock (this)
            {
                kernel = this.CreateKernel();

                kernel.Bind<IResolutionRoot>().ToConstant(kernel);
                kernel.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();
                kernel.Bind<IFilterProvider>().To<NinjectFilterAttributeFilterProvider>();
                kernel.Bind<IFilterProvider>().To<NinjectFilterProvider>();
                kernel.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
                kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
                kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
                kernel.Bind<ModelValidatorProvider>().To<NinjectDataAnnotationsModelValidatorProvider>();

                ModelValidatorProviders.Providers.Remove(ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().Single());
                DependencyResolver.SetResolver(this.CreateDependencyResolver());
                RemoveDefaultAttributeFilterProvider();

                kernel.Inject(this);

                if (kernel.Settings.Get("ReleaseScopeAtRequestEnd", true))
                {
                    OnePerRequestModule.StartManaging(kernel);
                }

                this.OnApplicationStarted();
            }
        }

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        public void Application_End()
        {
            lock (this)
            {
                if (kernel != null)
                {
                    kernel.Dispose();
                    kernel = null;
                }

                this.OnApplicationStopped();
            }
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
        protected IDependencyResolver CreateDependencyResolver()
        {
            return this.Kernel.Get<IDependencyResolver>();
        }

        /// <summary>
        /// Called when the application is started.
        /// </summary>
        protected virtual void OnApplicationStarted()
        {
        }

        /// <summary>
        /// Called when the application is stopped.
        /// </summary>
        protected virtual void OnApplicationStopped()
        {
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