//-------------------------------------------------------------------------------
// <copyright file="DistributedCacheFilter.cs" company="bbv Software Services AG">
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
    using System.Web.Mvc;
    using SampleApplication.Services.DistributedCacheService;

    /// <summary>
    /// A filter that caches the result of an action in a distributed cache.
    /// </summary>
    public class DistributedCacheFilter : IActionFilter
    {
        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly IDistributedCacheService cache;

        /// <summary>
        /// The expiration time of the cache entry.
        /// </summary>
        private readonly TimeSpan expirationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheFilter"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="expirationTime">The expiration time.</param>
        public DistributedCacheFilter(IDistributedCacheService cache, TimeSpan expirationTime)
        {
            this.cache = cache;
            this.expirationTime = expirationTime;
        }

        /// <summary>
        /// Called before an action method executes.
        /// Gets the cached result if available.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var result = this.cache.GetEntry(GetKey(filterContext.ActionDescriptor)) as ActionResult;
            if (result != null)
            {
                filterContext.Result = result;
            }
        }

        /// <summary>
        /// Called after the action method executes.
        /// Saves the result to the cache.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.cache.AddEntry(
                GetKey(filterContext.ActionDescriptor), 
                filterContext.Result,
                this.expirationTime);
        }

        /// <summary>
        /// Gets the key from the action descriptor.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The key for the action.</returns>
        private static string GetKey(ActionDescriptor actionDescriptor)
        {
            return string.Format(
                "{0}.{1}",
                actionDescriptor.ControllerDescriptor.ControllerName,
                actionDescriptor.ActionName);
        }
    }
}