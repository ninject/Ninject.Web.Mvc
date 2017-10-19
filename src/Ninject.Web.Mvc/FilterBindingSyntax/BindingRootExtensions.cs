// -------------------------------------------------------------------------------------------------
// <copyright file="BindingRootExtensions.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Ninject.Syntax;
    using Ninject.Web.Mvc.Filter;

    /// <summary>
    /// Extension methods for IBindingRoot to define filter bindings.
    /// </summary>
    public static class BindingRootExtensions
    {
        /// <summary>
        /// The key used to store the filter id in the binding metadata.
        /// </summary>
        public const string FilterIdMetadataKey = "FilterId";

        /// <summary>
        /// Creates a binding for a filter.
        /// </summary>
        /// <typeparam name="T">The type of the filter.</typeparam>
        /// <param name="kernel">The kernel.</param>
        /// <param name="scope">The filter scope.</param>
        /// <param name="order">The filter order.</param>
        /// <returns>The fluent syntax to specify more information for the binding.</returns>
        public static IFilterBindingWhenInNamedWithOrOnSyntax<T> BindFilter<T>(this IBindingRoot kernel, FilterScope scope, int? order)
        {
            var filterId = Guid.NewGuid();

            var filterBinding = kernel.Bind<T>().ToSelf();
            filterBinding.WithMetadata(FilterIdMetadataKey, filterId);

            var ninjectFilterBinding = kernel.Bind<INinjectFilter>().ToConstructor<NinjectFilter<T>>(
                x => new NinjectFilter<T>(x.Inject<IKernel>(), scope, order, filterId));
            return new FilterFilterBindingBuilder<T>(ninjectFilterBinding, filterBinding);
        }

        /// <summary>
        /// Indicates that the service should be bound to the specified constructor.
        /// </summary>
        /// <typeparam name="T">The type of the implementation.</typeparam>
        /// <param name="kernel">The kernel.</param>
        /// <param name="newExpression">The expression that specifies the constructor.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="order">The order.</param>
        /// <returns>The fluent syntax.</returns>
        public static IFilterBindingWhenInNamedWithOrOnSyntax<T> BindFilter<T>(
            this IBindingRoot kernel,
            Expression<Func<IConstructorArgumentSyntax, T>> newExpression,
            FilterScope scope,
            int? order)
        {
            var filterId = Guid.NewGuid();

            var filterBinding = kernel.Bind<T>().ToConstructor(newExpression);
            filterBinding.WithMetadata(FilterIdMetadataKey, filterId);

            var ninjectFilterBinding = kernel.Bind<INinjectFilter>().ToConstructor<NinjectFilter<T>>(
                x => new NinjectFilter<T>(x.Inject<IKernel>(), scope, order, filterId));
            return new FilterFilterBindingBuilder<T>(ninjectFilterBinding, filterBinding);
        }
    }
}