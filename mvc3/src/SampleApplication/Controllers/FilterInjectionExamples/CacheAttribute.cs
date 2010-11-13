//-------------------------------------------------------------------------------
// <copyright file="CacheAttribute.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Controllers.FilterInjectionExamples
{
    using System;

    /// <summary>
    /// Attribute to specify that the result shall be cached.
    /// </summary>
    public class CacheAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheAttribute"/> class.
        /// </summary>
        /// <param name="days">The days of the cache expiration time.</param>
        /// <param name="hours">The hours of the cache expiration time.</param>
        /// <param name="minutes">The minutes of the cache expiration time.</param>
        /// <param name="seconds">The seconds of the cache expiration time.</param>
        public CacheAttribute(int days, int hours, int minutes, int seconds)
        {
            this.Duration = new TimeSpan(days, hours, minutes, seconds);
        }

        /// <summary>
        /// Gets the duration until the cache entry expires.
        /// </summary>
        /// <value>The duration until the cache entry expires.</value>
        public TimeSpan Duration { get; private set; }
    }
}