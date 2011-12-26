//-------------------------------------------------------------------------------
// <copyright file="AttributeValueSelector.cs" company="Ninject Project Contributors">
//   Copyright (c) 2009-2011 Ninject Project Contributors
//   Authors: Remo Gloor (remo.gloor@gmail.com)
//           
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   you may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
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

    /// <summary>
    /// Syntax to specify which value from an attribute shall be passed to a constructor parameter.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    public class AttributeValueSelector<TAttribute>
        where TAttribute : Attribute
    {
        /// <summary>
        /// The attribute from which the value is returned.
        /// </summary>
        private readonly TAttribute attribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeValueSelector&lt;TAttribute&gt;"/> class.
        /// </summary>
        /// <param name="attribute">The attribute from which the value is returned.</param>
        public AttributeValueSelector(TAttribute attribute)
        {
            this.attribute = attribute;
        }

        /// <summary>
        /// Gets a value from the attribute.
        /// </summary>
        /// <typeparam name="T">The type of the returned value.</typeparam>
        /// <param name="valueSelector">The function that is used to get the value.</param>
        /// <returns>The selected value.</returns>
        public T GetValue<T>(Func<TAttribute, T> valueSelector)
        {
            return valueSelector(this.attribute);
        }
    }
}