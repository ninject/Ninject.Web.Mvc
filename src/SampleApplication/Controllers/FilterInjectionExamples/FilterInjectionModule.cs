//-------------------------------------------------------------------------------
// <copyright file="FilterInjectionModule.cs" company="bbv Software Services AG">
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
    using System.Linq;
    using System.Web.Mvc;

    using SampleApplication.Models.Movie;
    using SampleApplication.Services.DistributedCacheService;

    using log4net;
    using Ninject.Activation;
    using Ninject.Modules;
    using Ninject.Web.Mvc.Filter;
    using Ninject.Web.Mvc.FilterBindingSyntax;

    /// <summary>
    /// The ninject module for the filter injection example controller.
    /// </summary>
    public class FilterInjectionModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<ILog>().ToMethod(GetLogger);
            this.BindFilter<LogFilter>(FilterScope.Controller, 0).WithConstructorArgument("prefix", "A: "); // Every action of the application is logged
            this.BindFilter<LogFilter>(FilterScope.Controller, 0).WithConstructorArgument("prefix", "B: "); // Every action of the application is logged
            this.BindFilter(
                x => new DistributedCacheFilter(
                     x.Inject<IDistributedCacheService>(),
                     x.FromActionAttribute<CacheAttribute>().GetValue(attribute => attribute.Duration)), 
                     FilterScope.Action, 
                     0)
                .WhenActionMethodHas<CacheAttribute>();
            this.BindFilter<LogFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<LogAttribute>()
                .WithConstructorArgumentFromActionAttribute<LogAttribute>("prefix", a => a.Prefix);
            this.Bind<MoviesEntities>().ToConstructor(x => new MoviesEntities());
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <returns>The logger for the given context.</returns>
        private static ILog GetLogger(IContext ctx)
        {
            var filterContext = ctx.Request.ParentRequest.Parameters.OfType<FilterContextParameter>().SingleOrDefault();
            return LogManager.GetLogger(filterContext == null ? 
                ctx.Request.Target.Member.DeclaringType :
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerType);
        }
    }
}