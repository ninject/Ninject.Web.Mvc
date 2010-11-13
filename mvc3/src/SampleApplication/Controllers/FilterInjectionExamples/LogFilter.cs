//-------------------------------------------------------------------------------
// <copyright file="LogFilter.cs" company="bbv Software Services AG">
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
    using System.Web.Mvc;
    using log4net;

    /// <summary>
    /// A filter that loggs an actions.
    /// </summary>
    public class LogFilter : IActionFilter
    {
        /// <summary>
        /// The logger used to log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFilter"/> class.
        /// </summary>
        /// <param name="log">The logger used to log.</param>
        public LogFilter(ILog log)
        {
            this.log = log;
        }

        /// <summary>
        /// Called before an action method executes.
        /// Logs that an action is beeing executed.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.log.DebugFormat(
                "Executing action {0}.{1}", 
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, 
                filterContext.ActionDescriptor.ActionName);
        }

        /// <summary>
        /// Called after the action method executes.
        /// Logs that an action was executed.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.log.DebugFormat(
                "Executed action {0}.{1}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName);
        }
    }
}