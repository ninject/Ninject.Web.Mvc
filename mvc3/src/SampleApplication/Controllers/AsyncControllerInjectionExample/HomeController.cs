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

namespace SampleApplication.Controllers.AsyncControllerInjectionExample
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SampleApplication.Services.WelcomeMessageService;

    /// <summary>
    /// The controller of the home page.
    /// </summary>
    public class HomeController : AsyncController
    {
        /// <summary>
        /// The welcome message service
        /// </summary>
        private readonly IWelcomeMessageService welcomeMessageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="welcomeMessageService">The welcome message service.</param>
        public HomeController(IWelcomeMessageService welcomeMessageService)
        {
            this.welcomeMessageService = welcomeMessageService;
        }

        /// <summary>
        /// The index action start method.
        /// </summary>
        public void IndexAsync()
        {
        }

        /// <summary>
        /// The completion handler of the index action.
        /// </summary>
        /// <returns>The async result of the action</returns>
        public ActionResult IndexCompleted()
        {
            ViewModel.Message = this.welcomeMessageService.WelcomeMessage;
            return View();
        }

        /// <summary>
        /// The about action handler
        /// </summary>
        /// <returns>The result of the about action.</returns>
        public ActionResult About()
        {
            return View();
        }
    }
}
