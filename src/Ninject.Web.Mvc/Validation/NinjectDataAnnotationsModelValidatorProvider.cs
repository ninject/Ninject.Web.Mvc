// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectDataAnnotationsModelValidatorProvider.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// A DataAnnotationsModelValidatorProvider implementation that injects the validators.
    /// </summary>
    public class NinjectDataAnnotationsModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// The method info to get the attribute from the DataAnnotationsModelValidatorProvider
        /// </summary>
        private readonly MethodInfo getAttributeMethodInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDataAnnotationsModelValidatorProvider"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectDataAnnotationsModelValidatorProvider(IKernel kernel)
        {
            this.kernel = kernel;
            this.getAttributeMethodInfo =
                typeof(DataAnnotationsModelValidator).GetMethod("get_Attribute", BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets a list of validators.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attributes">The list of validation attributes.</param>
        /// <returns>A list of validators.</returns>
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            var validators = base.GetValidators(metadata, context, attributes);
            foreach (var modelValidator in validators.OfType<DataAnnotationsModelValidator>())
            {
                var attribute = this.getAttributeMethodInfo.Invoke(modelValidator, new object[0]);
                this.kernel.Inject(attribute);
            }

            return validators;
        }
    }
}