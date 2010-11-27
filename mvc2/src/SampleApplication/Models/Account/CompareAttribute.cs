//-------------------------------------------------------------------------------
// <copyright file="CompareAttribute.cs" company="bbv Software Services AG">
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

    /// <summary>
    /// Attribute to compare two properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class CompareAttribute : ValidationAttribute
    {
        /// <summary>
        /// The default error message
        /// </summary>
        private const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";

        /// <summary>
        /// The id of this validator
        /// </summary>
        private readonly object typeId = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareAttribute"/> class.
        /// </summary>
        /// <param name="confirmProperty">The confirm property name.</param>
        public CompareAttribute(string confirmProperty)
            : base(DefaultErrorMessage)
        {
            this.ConfirmProperty = confirmProperty;
        }

        /// <summary>
        /// Gets the confirm property.
        /// </summary>
        /// <value>The confirm property.</value>
        public string ConfirmProperty { get; private set; }

        /// <summary>
        /// When implemented in a derived class, gets a unique identifier for this <see cref="T:System.Attribute"/>.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Object"/> that is a unique identifier for the attribute.</returns>
        public override object TypeId
        {
            get
            {
                return this.typeId;
            }
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
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.ConfirmProperty);
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns>true if the validation is succesful; false otherwise.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var confirmValue = context.ObjectType.GetProperty(this.ConfirmProperty).GetValue(context.ObjectInstance, null);
            return !Equals(value, confirmValue) ? new ValidationResult(this.FormatErrorMessage(context.DisplayName)) : null;
        }
    }
}