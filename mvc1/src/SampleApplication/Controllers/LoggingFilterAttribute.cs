//-------------------------------------------------------------------------------
// <copyright file="LoggingFilterAttribute.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Controllers
{
    using System.Globalization;
    using System.Web.Mvc;
    using log4net;
    using Ninject;

    /// <summary>
    /// Filter attribute that logs the action.
    /// </summary>
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the logger that is used to log.
        /// </summary>
        /// <value>The logger that is used to log.</value>
        [Inject]
        public ILog Log { get; set; }

        /// <summary>
        /// Called by the MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Log.Info(string.Format(CultureInfo.InvariantCulture, "Executing: {0}", filterContext.ActionDescriptor.ActionName));
        }

        /// <summary>
        /// Called by the MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)  
        {  
            this.Log.Info(string.Format(CultureInfo.InvariantCulture, "Executed: {0}", filterContext.ActionDescriptor.ActionName));
        }
    }
}