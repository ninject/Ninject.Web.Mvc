// -------------------------------------------------------------------------------------------------
// <copyright file="MvcModule.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG. All rights reserved.
//   Copyright (c) 2010-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
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