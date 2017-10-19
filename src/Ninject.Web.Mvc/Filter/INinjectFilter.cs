// -------------------------------------------------------------------------------------------------
// <copyright file="INinjectFilter.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.Filter
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Used by the NinjectFilterProvider to get injected filters.
    /// </summary>
    public interface INinjectFilter
    {
        /// <summary>
        /// Builds the filter instances.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The created filters.</returns>
        IEnumerable<Filter> BuildFilters(FilterContextParameter parameter);
    }
}