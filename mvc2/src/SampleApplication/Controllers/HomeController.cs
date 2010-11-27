//-------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="bbv Software Services AG">
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
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;
    using System.Web.Mvc;

    /// <summary>
    /// The home controller.
    /// </summary>
    [HandleError]
    [LoggingFilter]
    public class HomeController : AsyncController
    {
        /// <summary>
        /// The data model of the home controller.
        /// </summary>
        private readonly IHomeControllerModel homeControllerModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="homeControllerModel">The home controller model.</param>
        public HomeController(IHomeControllerModel homeControllerModel)
        {
            this.homeControllerModel = homeControllerModel;
        }

        /// <summary>
        /// The index action acync begin
        /// </summary>
        public void IndexAsync()
        {
        }

        /// <summary>
        /// the index action async end
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>The view shown by the action</returns>
        public ActionResult IndexCompleted(IEnumerable<SyndicationItem> items)
        {
            ViewData["Message"] = this.homeControllerModel.WelcomeMessage;
            return View();
        }

        /// <summary>
        /// The about action handler
        /// </summary>
        /// <returns>The view shown by the about action.</returns>
        public ActionResult About()
        {
            return View();
        }
    }
}
