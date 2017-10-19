//-------------------------------------------------------------------------------
// <copyright file="IMembershipService.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Services.Account
{
    using System.Web.Security;

    /// <summary>
    /// The membership service
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Gets the length of the minimnum password.
        /// </summary>
        /// <value>The length of the minimum password.</value>
        int MinPasswordLength { get; }

        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the user credentials are valid, false otherwise.</returns>
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email address.</param>
        /// <returns>The status of the create operation.</returns>
        MembershipCreateStatus CreateUser(string userName, string password, string email);

        /// <summary>
        /// Changes the password of the specified user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>True if the change operation was successful, otherwise false</returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}