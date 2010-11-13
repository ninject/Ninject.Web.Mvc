//-------------------------------------------------------------------------------
// <copyright file="INinjectFilter.cs" company="bbv Software Services AG">
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
    /// Used by the NinjectFilterProvider to get injected filters.
    /// </summary>
    public interface INinjectFilter
    {
        /// <summary>
        /// Builds the filter instance.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The created filter.</returns>
        Filter BuildFilter(FilterContextParameter parameter);
    }
}