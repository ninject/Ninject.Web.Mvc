//-------------------------------------------------------------------------------
// <copyright file="NinjectFilter.cs" company="bbv Software Services AG">
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
    using System.Web.Mvc;

    /// <summary>
    /// Creates a filter of the specified type using ninject.
    /// </summary>
    /// <typeparam name="T">The type of the filter.</typeparam>
    public class NinjectFilter<T> : INinjectFilter
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Teh filter scope.
        /// </summary>
        private readonly FilterScope scope;

        /// <summary>
        /// The filter order.
        /// </summary>
        private readonly int? order;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="scope">The filter scope.</param>
        /// <param name="order">The filter order.</param>
        public NinjectFilter(IKernel kernel, FilterScope scope, int? order)
        {
            this.kernel = kernel;
            this.scope = scope;
            this.order = order;
        }

        /// <summary>
        /// Builds the filter instance.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The created filter.</returns>
        public Filter BuildFilter(FilterContextParameter parameter)
        {
            return new Filter(this.kernel.Get<T>(parameter), this.scope, this.order);
        }
    }
}