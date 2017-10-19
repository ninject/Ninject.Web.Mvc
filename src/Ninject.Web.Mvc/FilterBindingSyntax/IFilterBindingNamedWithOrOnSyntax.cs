// -------------------------------------------------------------------------------------------------
// <copyright file="IFilterBindingNamedWithOrOnSyntax.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    /// <summary>
    /// Used to set the scope, name, or add additional information or actions to a binding.
    /// </summary>
    /// <typeparam name="T">The service being bound.</typeparam>
    public interface IFilterBindingNamedWithOrOnSyntax<out T> :
        IFilterBindingNamedSyntax<T>,
        IFilterBindingWithSyntax<T>,
        IFilterBindingOnSyntax<T>
    {
    }
}