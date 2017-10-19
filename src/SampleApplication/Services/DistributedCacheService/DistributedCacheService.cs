//-------------------------------------------------------------------------------
// <copyright file="DistributedCacheService.cs" company="bbv Software Services AG">
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
    using System.Collections.Generic;

    /// <summary>
    /// The distributed cache service.
    /// </summary>
    public class DistributedCacheService : IDistributedCacheService
    {
        /// <summary>
        /// The date time provider.
        /// </summary>
        private readonly IDateTimeProvider dateTimeProvider;

        /// <summary>
        /// The cache entries.
        /// </summary>
        private readonly Dictionary<string, CacheEntry> cacheEntries = new Dictionary<string, CacheEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheService"/> class.
        /// </summary>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public DistributedCacheService(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Adds a new entry to the cache.
        /// </summary>
        /// <param name="key">The key of the entry.</param>
        /// <param name="value">The value.</param>
        /// <param name="expriationTime">The expriation time.</param>
        public void AddEntry(string key, object value, TimeSpan expriationTime)
        {
            this.cacheEntries[key] = new CacheEntry(this.dateTimeProvider.Now.Add(expriationTime), value);
        }

        /// <summary>
        /// Gets an entry from the cache.
        /// </summary>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value stored with the given key.</returns>
        public object GetEntry(string key)
        {
            CacheEntry cacheEntry;
            if (this.cacheEntries.TryGetValue(key, out cacheEntry))
            {
                if (this.dateTimeProvider.Now < cacheEntry.ExpriationTime)
                {
                    return cacheEntry.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Clears the entry with the given key.
        /// </summary>
        /// <param name="key">The key to be cleared.</param>
        public void ClearEntry(string key)
        {
            this.cacheEntries.Remove(key);
        }

        /// <summary>
        /// A cache entry.
        /// </summary>
        private class CacheEntry
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CacheEntry"/> class.
            /// </summary>
            /// <param name="expriationTime">The expriation time.</param>
            /// <param name="value">The value to be cached.</param>
            public CacheEntry(DateTime expriationTime, object value)
            {
                this.ExpriationTime = expriationTime;
                this.Value = value;
            }

            /// <summary>
            /// Gets the expriation time.
            /// </summary>
            /// <value>The expriation time.</value>
            public DateTime ExpriationTime { get; private set; }

            /// <summary>
            /// Gets the cached value.
            /// </summary>
            /// <value>The cached value.</value>
            public object Value { get; private set; }
        }
    }
}