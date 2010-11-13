//-------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Controllers.ControllerInjectionExample
{
    using System.Web.Mvc;
    using System.Web.Security;
    using SampleApplication.Models.Account;
    using SampleApplication.Services.Account;

    /// <summary>
    /// The account controller
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="formsAuthenticationService">The forms authentication service.</param>
        /// <param name="membershipService">The membership service.</param>
        public AccountController(
            IFormsAuthenticationService formsAuthenticationService, 
            IMembershipService membershipService)
        {
            this.MembershipService = membershipService;
            this.FormsService = formsAuthenticationService;
        }

        /// <summary>
        /// Gets or sets FormsService.
        /// </summary>
        public IFormsAuthenticationService FormsService { get; set; }

        /// <summary>
        /// Gets or sets MembershipService.
        /// </summary>
        public IMembershipService MembershipService { get; set; }

        /// <summary>
        /// The log on action
        /// </summary>
        /// <returns>The log on view.</returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Handles the log on request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>The home view in case of success. The log on view otherwise.</returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (this.MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    this.FormsService.SignIn(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return this.RedirectToAction("Index", "Home");
                }

                this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// THe log of action handler.
        /// </summary>
        /// <returns>Redirect to the home view.</returns>
        public ActionResult LogOff()
        {
            this.FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Handler for the register action.
        /// </summary>
        /// <returns>The register view.</returns>
        public ActionResult Register()
        {
            this.ViewModel.PasswordLength = this.MembershipService.MinPasswordLength;
            return View();
        }

        /// <summary>
        /// Handles the register request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The home view in case of success. The register view otherwise.</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = 
                    this.MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    this.FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, AccountValidationErrorMessages.ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            ViewModel.PasswordLength = this.MembershipService.MinPasswordLength;
            return View(model);
        }

        /// <summary>
        /// Handler for the change password action.
        /// </summary>
        /// <returns>The change password view.</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            this.ViewModel.PasswordLength = this.MembershipService.MinPasswordLength;
            return View();
        }

        /// <summary>
        /// Handler for the change password post request.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The change password success view in case of success. The change password view otherwise.</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.MembershipService.ChangePassword(this.User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }

                ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            this.ViewModel.PasswordLength = this.MembershipService.MinPasswordLength;
            return View(model);
        }

        /// <summary>
        /// Handles the change password success action
        /// </summary>
        /// <returns>The change password success view.</returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
