// -------------------------------------------------------------------------------------------------
// <copyright file="NinjectFilter.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Ninject.Web.Mvc.FilterBindingSyntax;

    /// <summary>
    /// Creates a filter of the specified type using ninject.
    /// </summary>
    /// <typeparam name="T">The type of the filter.</typeparam>
    public class NinjectFilter<T> : INinjectFilter
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Teh filter scope.
        /// </summary>
        private readonly FilterScope scope;

        /// <summary>
        /// The filter order.
        /// </summary>
        private readonly int? order;

        /// <summary>
        /// The id fo the filter.
        /// </summary>
        private readonly Guid filterId;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <param name="scope">The filter scope.</param>
        /// <param name="order">The filter order.</param>
        /// <param name="filterId">The filter id.</param>
        public NinjectFilter(IKernel kernel, FilterScope scope, int? order, Guid filterId)
        {
            this.kernel = kernel;
            this.scope = scope;
            this.order = order;
            this.filterId = filterId;
            this.NumberOfFilters = 1;
        }

        /// <summary>
        /// Gets or sets the number of filters created by this instance.
        /// </summary>
        /// <value>The number of filters.</value>
        public int NumberOfFilters { get; set; }

        /// <summary>
        /// Builds the filter instances.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The created filters.</returns>
        public IEnumerable<Filter> BuildFilters(FilterContextParameter parameter)
        {
            for (int i = 0; i < this.NumberOfFilters; i++)
            {
                parameter.AttributePosition = i;
                yield return new Filter(
                    this.kernel.Get<T>(m => m.Get(BindingRootExtensions.FilterIdMetadataKey, Guid.Empty).Equals(this.filterId), parameter),
                    this.scope,
                    this.order);
            }
        }
    }
}