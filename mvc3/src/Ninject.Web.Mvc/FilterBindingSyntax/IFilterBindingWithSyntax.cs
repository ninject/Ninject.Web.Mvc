//-------------------------------------------------------------------------------
// <copyright file="IFilterBindingWithSyntax.cs" company="bbv Software Services AG">
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
    using System.Web.Mvc;
    using Ninject.Activation;
    using Ninject.Parameters;
    using Ninject.Syntax;

    /// <summary>
    /// Used to add additional information to a binding.
    /// </summary>
    /// <typeparam name="T">The service being bound.</typeparam>
    public interface IFilterBindingWithSyntax<out T> : IBindingSyntax
    {
        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// </summary>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="value">The value for the argument.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithConstructorArgument(string name, object value);

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// </summary>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback to invoke to get the value for the argument.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithConstructorArgument(string name, Func<IContext, object> callback);

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// </summary>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="value">The value for the property.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithPropertyValue(string name, object value);

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// </summary>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The callback to invoke to get the value for the property.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithPropertyValue(string name, Func<IContext, object> callback);

        /// <summary>
        /// Adds a custom parameter to the binding.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithParameter(IParameter parameter);

        /// <summary>
        /// Sets the value of a piece of metadata on the binding.
        /// </summary>
        /// <param name="key">The metadata key.</param>
        /// <param name="value">The metadata value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithMetadata(string key, object value);

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// </summary>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithConstructorArgument(
            string name,
            Func<IContext, ControllerContext, ActionDescriptor, object> callback);

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// The value is retrieved from an attribute on the action of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>
        /// The fluent syntax to define more information
        /// </returns>
        IFilterBindingWithOrOnSyntax<T> WithConstructorArgumentFromActionAttribute<TAttribute>(
            string name,
            Func<TAttribute, object> callback);

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// The value is retrieved from an attribute on the controller of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>
        /// The fluent syntax to define more information
        /// </returns>
        IFilterBindingWithOrOnSyntax<T> WithConstructorArgumentFromControllerAttribute<TAttribute>(
            string name,
            Func<TAttribute, object> callback);

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// </summary>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The cllback to retrieve the value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithPropertyValue(
            string name,
            Func<IContext, ControllerContext, ActionDescriptor, object> callback);

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// The value is retrieved from an attribute on the action of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The cllback to retrieve the value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithPropertyValueFromActionAttribute<TAttribute>(
            string name,
            Func<TAttribute, object> callback);

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// The value is retrieved from an attribute on the controller of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The cllback to retrieve the value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> WithPropertyValueFromControllerAttribute<TAttribute>(
            string name,
            Func<TAttribute, object> callback);
    }
}