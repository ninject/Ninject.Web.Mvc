//-------------------------------------------------------------------------------
// <copyright file="ValidatePasswordLengthAttribute.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Security;

    /// <summary>
    /// Attribute to validate the password length.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// The default error message 
        /// </summary>
        private const string DefaultErrorMessage = "'{0}' must be at least {1} characters long.";

        /// <summary>
        /// The minimum number of characters for the password.
        /// </summary>
        private readonly int minimumCharacterCount = Membership.Provider.MinRequiredPasswordLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatePasswordLengthAttribute"/> class.
        /// </summary>
        public ValidatePasswordLengthAttribute()
            : base(DefaultErrorMessage)
        {
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.minimumCharacterCount);
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            return valueAsString != null && valueAsString.Length >= this.minimumCharacterCount;
        }
    }
}