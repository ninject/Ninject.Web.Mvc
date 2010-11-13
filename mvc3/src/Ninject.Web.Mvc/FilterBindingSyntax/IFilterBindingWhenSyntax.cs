//-------------------------------------------------------------------------------
// <copyright file="IFilterBindingWhenSyntax.cs" company="bbv Software Services AG">
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

    /// <summary>
    /// Used to add additional information or actions to a binding.
    /// </summary>
    /// <typeparam name="T">The type of the service</typeparam>
    public interface IFilterBindingWhenSyntax<out T>
    {
        /// <summary>
        /// Indicates that the binding should be used only for requests that support the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> When(Func<IRequest, bool> condition);

        /// <summary>
        /// Indicates that the binding should be used only for requests that support the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> When(Func<ControllerContext, ActionDescriptor, bool> condition);

        /// <summary>
        /// Indicates that the binding should be used only when the action method has
        /// an attribute of the specified type.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenActionMethodHas(Type attributeType);

        /// <summary>
        /// Indicates that the binding should be used only when the action method has
        /// an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenActionMethodHas<TAttribute>();

        /// <summary>
        /// Indicates that the binding should be used only when the controller has
        /// an attribute of the specified type.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerHas(Type attributeType);

        /// <summary>
        /// Indicates that the binding should be used only when the controller has
        /// an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerHas<TAttribute>();

        /// <summary>
        /// Whens the type of the controller.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerType(Type controllerType);

        /// <summary>
        /// Whens the type of the controller.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerType<TAttribute>();
    }
}