// -------------------------------------------------------------------------------------------------
// <copyright file="MvcModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc
{
    using System.Web.Mvc;

    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc.Filter;
    using Ninject.Web.Mvc.Validation;

    /// <summary>
    /// Defines the bindings and plugins of the MVC web extension.
    /// </summary>
    public class MvcModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectMvcHttpApplicationPlugin>();
            this.Kernel.Bind<IDependencyResolver>().To<NinjectDependencyResolver>();
            this.Kernel.Bind<IFilterProvider>().To<NinjectFilterAttributeFilterProvider>();
            this.Kernel.Bind<IFilterProvider>().To<NinjectFilterProvider>();
            this.Kernel.Bind<ModelValidatorProvider>().To<NinjectDataAnnotationsModelValidatorProvider>();
        }
    }
}