// -------------------------------------------------------------------------------------------------
// <copyright file="IConstructorArgumentSyntaxExtensions.cs" company="Ninject Project Contributors">
//   Copyright (c) 2010 bbv Software Services AG.
//   Copyright (c) 2010-2017 Ninject Contributors.
//   Licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Web.Mvc.FilterBindingSyntax
{
    using System;
    using System.Linq;

    using Ninject.Syntax;
    using Ninject.Web.Mvc.Filter;

    /// <summary>
    /// Extension methods for IConstructorArgumentSyntax
    /// </summary>
    public static class IConstructorArgumentSyntaxExtensions
    {
        /// <summary>
        /// Specifies that the constructor parameter shall be received from an attribute on the action.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="syntax">The constructor argument syntax.</param>
        /// <returns>The syntax to specify which value to use.</returns>
        public static AttributeValueSelector<TAttribute> FromActionAttribute<TAttribute>(this IConstructorArgumentSyntax syntax)
            where TAttribute : Attribute
        {
            return new AttributeValueSelector<TAttribute>(
                syntax.Context.Parameters.OfType<FilterContextParameter>().Single().ActionDescriptor // use existin impl
                .GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().Single());
        }

        /// <summary>
        /// Specifies that the constructor parameter shall be received from an attribute on the controller.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="syntax">The constructor argument syntax.</param>
        /// <returns>The syntax to specify which value to use.</returns>
        public static AttributeValueSelector<TAttribute> FromControllerAttribute<TAttribute>(this IConstructorArgumentSyntax syntax)
            where TAttribute : Attribute
        {
            return new AttributeValueSelector<TAttribute>(
                syntax.Context.Parameters.OfType<FilterContextParameter>().Single().ActionDescriptor.ControllerDescriptor
                .GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().Single());
        }
    }
}