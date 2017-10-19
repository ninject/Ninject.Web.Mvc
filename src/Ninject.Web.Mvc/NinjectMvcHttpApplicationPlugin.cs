// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectMvcHttpApplicationPlugin.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc
{
    using System.Linq;
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
        /// The ninject kernel.
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
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            ModelValidatorProviders.Providers.Remove(ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().Single());
            DependencyResolver.SetResolver(this.CreateDependencyResolver());
            RemoveDefaultAttributeFilterProvider();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
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
        /// Creates the controller factory that is used to create the controllers.
        /// </summary>
        /// <returns>The created controller factory.</returns>
        protected IDependencyResolver CreateDependencyResolver()
        {
            return this.kernel.Get<IDependencyResolver>();
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