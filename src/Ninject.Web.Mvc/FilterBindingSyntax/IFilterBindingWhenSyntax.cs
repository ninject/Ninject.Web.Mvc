// -------------------------------------------------------------------------------------------------
// <copyright file="IFilterBindingWhenSyntax.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

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
        /// Indicates that the binding should be used only when the action method has
        /// no attribute of the specified type.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenActionMethodHasNo(Type attributeType);

        /// <summary>
        /// Indicates that the binding should be used only when the action method has
        /// no attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenActionMethodHasNo<TAttribute>();

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
        /// Indicates that the binding should be used only when the controller has
        /// no attribute of the specified type.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerHasNo(Type attributeType);

        /// <summary>
        /// Indicates that the binding should be used only when the controller has
        /// no attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerHasNo<TAttribute>();

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