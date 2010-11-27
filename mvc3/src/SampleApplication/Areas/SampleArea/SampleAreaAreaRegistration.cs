//-------------------------------------------------------------------------------
// <copyright file="SampleAreaAreaRegistration.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Areas.SampleArea
{
    using System.Web.Mvc;

    /// <summary>
    /// The registration for the sample area
    /// </summary>
    public class SampleAreaAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to be registered.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the area to be registered.</returns>
        public override string AreaName
        {
            get
            {
                return "SampleArea";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SampleArea_default",
                "SampleArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}
