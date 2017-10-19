// -------------------------------------------------------------------------------------------------
// <copyright file="FilterContextParameter.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.Filter
{
    using System.Web.Mvc;

    using Ninject.Parameters;

    /// <summary>
    /// A parameter that contains the controller context and action descriptor for the filter.
    /// </summary>
    public class FilterContextParameter : Parameter
    {
        /// <summary>
        /// The name of the parameter.
        /// </summary>
        public const string ParameterName = "FilterContext";

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterContextParameter"/> class.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        public FilterContextParameter(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            : base(ParameterName, ctx => null, false)
        {
            this.ControllerContext = controllerContext;
            this.ActionDescriptor = actionDescriptor;
        }

        /// <summary>
        /// Gets or sets the position of the attribute used to get constructor values from.
        /// </summary>
        /// <value>The attribute position.</value>
        public int AttributePosition { get; set; }

        /// <summary>
        /// Gets the controller context.
        /// </summary>
        /// <value>The controller context.</value>
        public ControllerContext ControllerContext { get; private set; }

        /// <summary>
        /// Gets the action descriptor.
        /// </summary>
        /// <value>The action descriptor.</value>
        public ActionDescriptor ActionDescriptor { get; private set; }
    }
}