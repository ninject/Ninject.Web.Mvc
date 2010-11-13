//-------------------------------------------------------------------------------
// <copyright file="NinjectFilterProvider.cs" company="bbv Software Services AG">
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

namespace Ninject.Web.Mvc.Filter
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// A filter provider that gets the filter by requesting all INinjectFilters.
    /// </summary>
    public class NinjectFilterProvider : IFilterProvider
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectFilterProvider"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectFilterProvider(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>All filters defined on the kernel.</returns>
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var parameter = new FilterContextParameter(controllerContext, actionDescriptor);
            return this.kernel.GetAll<INinjectFilter>(parameter).Select(filter => filter.BuildFilter(parameter));
        }
    }
}