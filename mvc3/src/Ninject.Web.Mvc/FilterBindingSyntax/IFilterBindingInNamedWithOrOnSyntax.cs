//-------------------------------------------------------------------------------
// <copyright file="IFilterBindingInNamedWithOrOnSyntax.cs" company="bbv Software Services AG">
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
    /// <summary>
    /// Used to set the scope, name, or add additional information or actions to a binding.
    /// </summary>
    /// <typeparam name="T">The service being bound.</typeparam>
    public interface IFilterBindingInNamedWithOrOnSyntax<out T> : 
        IFilterBindingInSyntax<T>, 
        IFilterBindingNamedSyntax<T>,
        IFilterBindingWithSyntax<T>, 
        IFilterBindingOnSyntax<T>
    {
    }
}