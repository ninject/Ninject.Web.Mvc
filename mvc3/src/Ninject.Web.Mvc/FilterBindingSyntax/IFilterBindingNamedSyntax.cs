//-------------------------------------------------------------------------------
// <copyright file="IFilterBindingNamedSyntax.cs" company="bbv Software Services AG">
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