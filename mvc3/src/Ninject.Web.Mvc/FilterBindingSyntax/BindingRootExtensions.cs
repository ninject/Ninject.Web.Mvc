//-------------------------------------------------------------------------------
// <copyright file="BindingRootExtensions.cs" company="bbv Software Services AG">
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

namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Ninject.Planning.Bindings;
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