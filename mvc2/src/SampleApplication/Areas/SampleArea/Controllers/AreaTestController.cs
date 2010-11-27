//-------------------------------------------------------------------------------
// <copyright file="AreaTestController.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Areas.SampleArea.Controllers
{
    using System.Web.Mvc;
    using SampleApplication.Controllers;

    /// <summary>
    /// Test controller for testing areas
    /// </summary>
    public class AreaTestController : Controller
    {
        /// <summary>
        /// The model from which the view model is initalized.
        /// </summary>
        private readonly IHomeControllerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaTestController"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public AreaTestController(IHomeControllerModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Index action of the controller
        /// </summary>
        /// <returns>The view action result.</returns>
        public ActionResult Index()
        {
            ViewData["Message"] = this.model.WelcomeMessage;
            return View();
        }
    }
}
