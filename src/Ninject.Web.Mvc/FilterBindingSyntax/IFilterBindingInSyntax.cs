// -------------------------------------------------------------------------------------------------
// <copyright file="IFilterBindingInSyntax.cs" company="Ninject Project Contributors">
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
    using Ninject.Syntax;

    /// <summary>
    /// Used to define the scope in which instances activated via a binding should be re-used.
    /// </summary>
    /// <typeparam name="T">The service being bound.</typeparam>
    public interface IFilterBindingInSyntax<out T> : IBindingSyntax
    {
        /// <summary>
        /// Indicates that only a single instance of the binding should be created, and then
        /// should be re-used for all subsequent requests.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingNamedWithOrOnSyntax<T> InSingletonScope();

        /// <summary>
        /// Indicates that instances activated via the binding should not be re-used, nor have
        /// their lifecycle managed by Ninject.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingNamedWithOrOnSyntax<T> InTransientScope();

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used within the same thread.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingNamedWithOrOnSyntax<T> InThreadScope();

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used within the same
        /// HTTP request.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingNamedWithOrOnSyntax<T> InRequestScope();

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used as long as the object
        /// returned by the provided callback remains alive (that is, has not been garbage collected).
        /// </summary>
        /// <param name="scope">The callback that returns the scope.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, object> scope);

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used as long as the object
        /// returned by the provided callback remains alive (that is, has not been garbage collected).
        /// </summary>
        /// <param name="scope">The callback that returns the scope.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, ControllerContext, ActionDescriptor, object> scope);
    }
}