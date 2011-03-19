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
    using System;
    using System.Globalization;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Security;

    using SampleApplication.Services.Account;

    /// <summary>
    /// The account controller.
    /// </summary>
    [HandleError]
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
            this.FormsAuthenticationService = formsAuthenticationService;
        }

        /// <summary>
        /// Gets the forms authentication service.
        /// </summary>
        public IFormsAuthenticationService FormsAuthenticationService { get; private set; }

        /// <summary>
        /// Gets the membership service.
        /// </summary>
        public IMembershipService MembershipService { get; private set; }

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
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">if set to <c>true</c> the user is remembered.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// The home view in case of success. The log on view otherwise.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl)
        {
            if (!this.ValidateLogOn(userName, password))
            {
                return View();
            }

            this.FormsAuthenticationService.SignIn(userName, rememberMe);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The log of action handler.
        /// </summary>
        /// <returns>Redirect to the home view.</returns>
        public ActionResult LogOff()
        {
            this.FormsAuthenticationService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Handler for the register action.
        /// </summary>
        /// <returns>The register view.</returns>
        public ActionResult Register()
        {
            this.ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;
            return View();
        }

        /// <summary>
        /// Handles the register request.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The password confirmation.</param>
        /// <returns>
        /// The home view in case of success. The register view otherwise.
        /// </returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string userName, string email, string password, string confirmPassword)
        {
            this.ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;

            if (this.ValidateRegistration(userName, email, password, confirmPassword))
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = this.MembershipService.CreateUser(userName, password, email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    this.FormsAuthenticationService.SignIn(userName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        /// <summary>
        /// Handler for the change password action.
        /// </summary>
        /// <returns>The change password view.</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            this.ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;
            return View();
        }

        /// <summary>
        /// Handler for the change password post request.
        /// </summary>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The password confirmation.</param>
        /// <returns>
        /// The change password success view in case of success. The change password view otherwise.
        /// </returns>
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Exceptions result in password not being changed.")]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            this.ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;

            if (!this.ValidateChangePassword(currentPassword, newPassword, confirmPassword))
            {
                return View();
            }

            try
            {
                if (this.MembershipService.ChangePassword(this.User.Identity.Name, currentPassword, newPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }

                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View();
            }
            catch
            {
                ModelState.AddModelError("_FORM", "The current password is incorrect or the new password is invalid.");
                return View();
            }
        }

        /// <summary>
        /// Handles the change password success action
        /// </summary>
        /// <returns>The change password success view.</returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        /// <summary>
        /// Method called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Contains information about the current request and action.</param>
        /// <exception cref="InvalidOperationException">Windows authentication is not supported.</exception>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        /// <summary>
        /// Validates the change password data.
        /// </summary>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>True if the change password data is valid. Otherwise false.</returns>
        private bool ValidateChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(currentPassword))
            {
                ModelState.AddModelError("currentPassword", "You must specify a current password.");
            }

            if (newPassword == null || newPassword.Length < this.MembershipService.MinPasswordLength)
            {
                string errorMessage = String.Format(
                    CultureInfo.CurrentCulture, 
                    "You must specify a new password of {0} or more characters.", 
                    this.MembershipService.MinPasswordLength);
                ModelState.AddModelError(
                    "newPassword",
                    errorMessage);
            }

            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        /// <summary>
        /// Validates the log on data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the log on data is valid. Otherwise false.</returns>
        private bool ValidateLogOn(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }

            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "You must specify a password.");
            }

            if (!this.MembershipService.ValidateUser(userName, password))
            {
                ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
            }

            return ModelState.IsValid;
        }

        /// <summary>
        /// Validates the registration data.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The password confirmation.</param>
        /// <returns>True if the registration data is valid. Otherwise false.</returns>
        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }

            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }

            if (password == null || password.Length < this.MembershipService.MinPasswordLength)
            {
                string errorMessage = String.Format(
                    CultureInfo.CurrentCulture,
                    "You must specify a password of {0} or more characters.",
                    this.MembershipService.MinPasswordLength);
                ModelState.AddModelError(
                    "password",
                    errorMessage);
            }

            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            return ModelState.IsValid;
        }

        /// <summary>
        /// Converts the error code to the error string.
        /// </summary>
        /// <param name="createStatus">The create status.</param>
        /// <returns>The error message.</returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
