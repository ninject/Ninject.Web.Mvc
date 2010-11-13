//-------------------------------------------------------------------------------
// <copyright file="IDistributedCacheService.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Services.DistributedCacheService
{
    using System;

    /// <summary>
    /// A distributed cache service.
    /// </summary>
    public interface IDistributedCacheService
    {
        /// <summary>
        /// Adds a new entry to the cache.
        /// </summary>
        /// <param name="key">The key of the entry.</param>
        /// <param name="value">The value.</param>
        /// <param name="expriationTime">The expriation time.</param>
        void AddEntry(string key, object value, TimeSpan expriationTime);

        /// <summary>
        /// Gets an entry from the cache.
        /// </summary>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value stored with the given key.</returns>
        object GetEntry(string key);

        /// <summary>
        /// Clears the entry with the given key.
        /// </summary>
        /// <param name="key">The key to be cleared.</param>
        void ClearEntry(string key);
    }
}