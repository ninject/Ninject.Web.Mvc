// 
// Authors: Nate Kohari <nate@enkari.com>, Remo Gloor <remo.gloor@gmail.com>, Josh Close <narshe@gmail.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

namespace Ninject.Web.Mvc
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ninject.Infrastructure;
    using Ninject.Planning.Bindings.Resolvers;

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

                kernel.Components.RemoveAll<IMissingBindingResolver>();
                kernel.Components.Add<IMissingBindingResolver, ControllerMissingBindingResolver>();
                kernel.Components.Add<IMissingBindingResolver, SelfBindingResolver>();

                kernel.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
                kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
                kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
                kernel.Bind<IFilterInjector>().To<FilterInjector>().InSingletonScope();
                
                ControllerBuilder.Current.SetControllerFactory(this.CreateControllerFactory());

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
        protected virtual NinjectControllerFactory CreateControllerFactory()
        {
            return new NinjectControllerFactory(this.Kernel);
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
    }
}