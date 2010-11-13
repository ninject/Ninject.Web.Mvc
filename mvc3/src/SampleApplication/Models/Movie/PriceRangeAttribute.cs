//-------------------------------------------------------------------------------
// <copyright file="PriceRangeAttribute.cs" company="bbv Software Services AG">
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

namespace SampleApplication.Models.Movie
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Ninject;
    using SampleApplication.Services.PriceingService;

    /// <summary>
    /// Vaildation attribute that verifies that the price is in the range given by the priceing service.
    /// Used to demonstrate injection of validation attributes.
    /// </summary>
    public class PriceRangeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the priceing service.
        /// </summary>
        /// <value>The priceing service.</value>
        [Inject]
        public IPriceingService PriceingService { get; set; }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.PriceingService.MinimumPrice, this.PriceingService.MaximumPrice);
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
            if (value == null)
            {
                return true;
            }

            if ((value is string) && string.IsNullOrEmpty((string)value))
            {
                return true;
            }

            var intValue = Convert.ToInt32(value);
            return (intValue >= this.PriceingService.MinimumPrice) && (intValue <= this.PriceingService.MaximumPrice);         
        }
    }
}