//-------------------------------------------------------------------------------
// <copyright file="IPriceingService.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Services.PriceingService
{
    /// <summary>
    /// A service that is responsible for telling the minimum and maximum prices of movies.
    /// </summary>
    public interface IPriceingService
    {
        /// <summary>
        /// Gets the minimum price.
        /// </summary>
        /// <value>The minimum price.</value>
        int MinimumPrice { get; }

        /// <summary>
        /// Gets the maximum price.
        /// </summary>
        /// <value>The maximum price.</value>
        int MaximumPrice { get; }
    }
}