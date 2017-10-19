// -------------------------------------------------------------------------------------------------
// <copyright file="IFilterBindingOnSyntax.cs" company="Ninject Project Contributors">
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
    /// Used to add additional actions to be performed during activation or deactivation of instances via a binding.
    /// </summary>
    /// <typeparam name="T">The service being bound.</typeparam>
    public interface IFilterBindingOnSyntax<out T> : IBindingSyntax
    {
        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are activated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingOnSyntax<T> OnActivation(Action<T> action);

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are activated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingOnSyntax<T> OnActivation(Action<IContext, T> action);

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are deactivated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingOnSyntax<T> OnDeactivation(Action<T> action);

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are deactivated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingOnSyntax<T> OnDeactivation(Action<IContext, T> action);

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are activated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingOnSyntax<T> OnActivation(Action<IContext, ControllerContext, ActionDescriptor, T> action);
    }
}