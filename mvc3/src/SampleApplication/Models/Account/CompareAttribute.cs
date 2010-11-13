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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class CompareAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
        private readonly object _typeId = new object();

        public CompareAttribute(string confirmProperty)
            : base(_defaultErrorMessage)
        {
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, ConfirmProperty);
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var confirmValue = context.ObjectType.GetProperty(ConfirmProperty).GetValue(context.ObjectInstance, null);
            if (!Equals(value, confirmValue))
            {
                return new ValidationResult(FormatErrorMessage(context.DisplayName));
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]{
                            new ModelClientValidationEqualToRule(FormatErrorMessage(metadata.GetDisplayName()), ConfirmProperty)
                        };
        }
    }
}