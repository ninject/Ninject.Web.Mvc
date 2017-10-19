// -------------------------------------------------------------------------------------------------
// <copyright file="IFilterBindingNamedSyntax.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    using Ninject.Syntax;

    /// <summary>
    /// Used to define the name of a binding.
    /// </summary>
    /// <typeparam name="T">The service being bound.</typeparam>
    public interface IFilterBindingNamedSyntax<out T> : IBindingSyntax
    {
        /// <summary>
        /// Indicates that the binding should be registered with the specified name. Names are not
        /// necessarily unique; multiple bindings for a given service may be registered with the same name.
        /// </summary>
        /// <param name="name">The name to give the binding.</param>
        /// <returns>The fluent syntax to define more information</returns>
        IFilterBindingWithOrOnSyntax<T> Named(string name);
    }
}