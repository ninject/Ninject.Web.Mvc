//-------------------------------------------------------------------------------
// <copyright file="FilterFilterBindingBuilder.cs" company="bbv Software Services AG">
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
    using System.Linq;
    using System.Web.Mvc;
    using Ninject.Activation;
    using Ninject.Parameters;
    using Ninject.Planning.Bindings;
    using Ninject.Syntax;
    using Ninject.Web.Mvc.Filter;

    /// <summary>
    /// Binding builder for filters.
    /// </summary>
    /// <typeparam name="T">The type of the filter.</typeparam>
    public class FilterFilterBindingBuilder<T> :
        IFilterBindingWhenSyntax<T>, 
        IFilterBindingInNamedWithOrOnSyntax<T>, 
        IFilterBindingNamedWithOrOnSyntax<T>, 
        IFilterBindingWithOrOnSyntax<T>
    {
        /// <summary>
        /// The binding of the ninject filter. Conditions are added here.
        /// </summary>
        private readonly IBindingWhenInNamedWithOrOnSyntax<INinjectFilter> ninjectFilterBindingSyntax;

        /// <summary>
        /// The binding of the filter. All other additionla configuration but the conditions are added here.
        /// </summary>
        private readonly IBindingWhenInNamedWithOrOnSyntax<T> filterBindingSyntax;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterFilterBindingBuilder&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="ninjectFilterBindingSyntax">The ninject filter binding syntax.</param>
        /// <param name="filterBindingSyntax">The filter binding syntax.</param>
        public FilterFilterBindingBuilder(
            IBindingWhenInNamedWithOrOnSyntax<INinjectFilter> ninjectFilterBindingSyntax, 
            IBindingWhenInNamedWithOrOnSyntax<T> filterBindingSyntax)
        {
            this.ninjectFilterBindingSyntax = ninjectFilterBindingSyntax;
            this.filterBindingSyntax = filterBindingSyntax;
        }

        /// <summary>
        /// Gets the binding.
        /// </summary>
        /// <value>The binding.</value>
        public IBinding Binding
        {
            get
            {
                return this.filterBindingSyntax.Binding;
            }
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        public IKernel Kernel
        {
            get
            {
                return this.ninjectFilterBindingSyntax.Kernel;
            }
        }

        /// <summary>
        /// Indicates that the binding should be registered with the specified name. Names are not
        /// necessarily unique; multiple bindings for a given service may be registered with the same name.
        /// </summary>
        /// <param name="name">The name to give the binding.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> Named(string name)
        {
            this.filterBindingSyntax.Named(name);
            return this;
        }

        /// <summary>
        /// Indicates that the binding should be used only for requests that support the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> When(Func<IRequest, bool> condition)
        {
            this.ninjectFilterBindingSyntax.When(condition);
            return this;
        }

        /// <summary>
        /// Indicates that the binding should be used only for requests that support the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> When(Func<ControllerContext, ActionDescriptor, bool> condition)
        {
            this.When(ctx =>
            {
                var parameter = (FilterContextParameter)ctx.Parameters.Single(p => p is FilterContextParameter);
                return condition(parameter.ControllerContext, parameter.ActionDescriptor);
            });

            return this;
        }

        /// <summary>
        /// Indicates that the binding should be used only when the action method has
        /// an attribute of the specified type.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> WhenActionMethodHas(Type attributeType)
        {
            this.When((controllerContext, actionDescriptor) =>
                actionDescriptor.GetCustomAttributes(attributeType, true).Any());
            return this;
        }

        /// <summary>
        /// Indicates that the binding should be used only when the action method has
        /// an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> WhenActionMethodHas<TAttribute>()
        {
            this.WhenActionMethodHas(typeof(TAttribute));
            return this;
        }

        /// <summary>
        /// Indicates that the binding should be used only when the controller has
        /// an attribute of the specified type.
        /// </summary>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerHas(Type attributeType)
        {
            this.When((controllerContext, actionDescriptor) =>
                actionDescriptor.ControllerDescriptor.GetCustomAttributes(attributeType, true).Any());
            return this;
        }

        /// <summary>
        /// Indicates that the binding should be used only when the controller has
        /// an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerHas<TAttribute>()
        {
            this.WhenControllerHas(typeof(TAttribute));
            return this;
        }

        /// <summary>
        /// Whens the type of the controller.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerType(Type controllerType)
        {
            this.When(
                (controllerContext, actionDescriptor) =>
                    actionDescriptor.ControllerDescriptor.ControllerType == controllerType);
            return this;
        }

        /// <summary>
        /// Whens the type of the controller.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingInNamedWithOrOnSyntax<T> WhenControllerType<TAttribute>()
        {
            this.WhenControllerType(typeof(TAttribute));
            return this;
        }

        /// <summary>
        /// Indicates that only a single instance of the binding should be created, and then
        /// should be re-used for all subsequent requests.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingNamedWithOrOnSyntax<T> InSingletonScope()
        {
            this.filterBindingSyntax.InSingletonScope();
            return this;
        }

        /// <summary>
        /// Indicates that instances activated via the binding should not be re-used, nor have
        /// their lifecycle managed by Ninject.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingNamedWithOrOnSyntax<T> InTransientScope()
        {
            this.filterBindingSyntax.InTransientScope();
            return this;
        }

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used within the same thread.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingNamedWithOrOnSyntax<T> InThreadScope()
        {
            this.filterBindingSyntax.InThreadScope();
            return this;
        }

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used within the same
        /// HTTP request.
        /// </summary>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingNamedWithOrOnSyntax<T> InRequestScope()
        {
            this.filterBindingSyntax.InRequestScope();
            return this;
        }

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used as long as the object
        /// returned by the provided callback remains alive (that is, has not been garbage collected).
        /// </summary>
        /// <param name="scope">The callback that returns the scope.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, object> scope)
        {
            this.filterBindingSyntax.InScope(scope);
            return this;
        }

        /// <summary>
        /// Indicates that instances activated via the binding should be re-used as long as the object
        /// returned by the provided callback remains alive (that is, has not been garbage collected).
        /// </summary>
        /// <param name="scope">The callback that returns the scope.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingNamedWithOrOnSyntax<T> InScope(Func<IContext, ControllerContext, ActionDescriptor, object> scope)
        {
            this.filterBindingSyntax.InScope(ctx =>
                {
                    var parameter = GetFilterContextParameter(ctx);
                    return scope(ctx, parameter.ControllerContext, parameter.ActionDescriptor);
                });
            return this;
        }

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// </summary>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="value">The value for the argument.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithConstructorArgument(string name, object value)
        {
            this.filterBindingSyntax.WithConstructorArgument(name, value);
            return this;
        }

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// </summary>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback to invoke to get the value for the argument.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithConstructorArgument(string name, Func<IContext, object> callback)
        {
            this.filterBindingSyntax.WithConstructorArgument(name, callback);
            return this;
        }

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// </summary>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="value">The value for the property.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithPropertyValue(string name, object value)
        {
            this.filterBindingSyntax.WithPropertyValue(name, value);
            return this;
        }

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// </summary>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The callback to invoke to get the value for the property.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithPropertyValue(string name, Func<IContext, object> callback)
        {
            this.filterBindingSyntax.WithPropertyValue(name, callback);
            return this;
        }

        /// <summary>
        /// Adds a custom parameter to the binding.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithParameter(IParameter parameter)
        {
            this.filterBindingSyntax.WithParameter(parameter);
            return this;
        }

        /// <summary>
        /// Sets the value of a piece of metadata on the binding.
        /// </summary>
        /// <param name="key">The metadata key.</param>
        /// <param name="value">The metadata value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithMetadata(string key, object value)
        {
            this.filterBindingSyntax.WithMetadata(key, value);
            return this;
        }

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// </summary>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithConstructorArgument(
            string name,
            Func<IContext, ControllerContext, ActionDescriptor, object> callback)
        {
            return this.WithConstructorArgument(
                name,
                ctx =>
                {
                    var parameter = GetFilterContextParameter(ctx);
                    return callback(ctx, parameter.ControllerContext, parameter.ActionDescriptor);
                });
        }

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// The value is retrieved from an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>
        /// The fluent syntax to define more information
        /// </returns>
        public IFilterBindingWithOrOnSyntax<T> WithConstructorArgumentFromActionAttribute<TAttribute>(
            string name,
            Func<TAttribute, object> callback)
        {
            return this.WithConstructorArgument(
                name,
                (ctx, controllerContext, actionDescriptor) =>
                callback((TAttribute)actionDescriptor.GetCustomAttributes(typeof(TAttribute), true).Single()));
        }

        /// <summary>
        /// Indicates that the specified constructor argument should be overridden with the specified value.
        /// The value is retrieved from an attribute on the controller of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the argument to override.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>
        /// The fluent syntax to define more information
        /// </returns>
        public IFilterBindingWithOrOnSyntax<T> WithConstructorArgumentFromControllerAttribute<TAttribute>(string name, Func<TAttribute, object> callback)
        {
            return this.WithConstructorArgument(
                name,
                (ctx, controllerContext, actionDescriptor) =>
                callback((TAttribute)actionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(TAttribute), true).Single()));
        }

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// </summary>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The cllback to retrieve the value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithPropertyValue(
            string name,
            Func<IContext, ControllerContext, ActionDescriptor, object> callback)
        {
            return this.WithPropertyValue(
                name,
                ctx =>
                {
                    var parameter = GetFilterContextParameter(ctx);
                    return callback(ctx, parameter.ControllerContext, parameter.ActionDescriptor);
                });
        }

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// The value is retrieved from an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The cllback to retrieve the value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithPropertyValueFromActionAttribute<TAttribute>(
            string name,
            Func<TAttribute, object> callback)
        {
            return this.WithPropertyValue(
                name,
                (ctx, controllerContext, actionDescriptor) =>
                callback((TAttribute)actionDescriptor.GetCustomAttributes(typeof(TAttribute), true).Single()));
        }

        /// <summary>
        /// Indicates that the specified property should be injected with the specified value.
        /// The value is retrieved from an attribute on the controller of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="name">The name of the property to override.</param>
        /// <param name="callback">The cllback to retrieve the value.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingWithOrOnSyntax<T> WithPropertyValueFromControllerAttribute<TAttribute>(string name, Func<TAttribute, object> callback)
        {
            return this.WithPropertyValue(
                name,
                (ctx, controllerContext, actionDescriptor) =>
                callback((TAttribute)actionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(TAttribute), true).Single()));
        }

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are activated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingOnSyntax<T> OnActivation(Action<T> action)
        {
            this.filterBindingSyntax.OnActivation(action);
            return this;
        }

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are activated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingOnSyntax<T> OnActivation(Action<IContext, T> action)
        {
            this.filterBindingSyntax.OnActivation(action);
            return this;
        }

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are deactivated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingOnSyntax<T> OnDeactivation(Action<IContext, T> action)
        {
            this.filterBindingSyntax.OnDeactivation(action);
            return this;
        }

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are deactivated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingOnSyntax<T> OnDeactivation(Action<T> action)
        {
            this.filterBindingSyntax.OnDeactivation(action);
            return this;
        }

        /// <summary>
        /// Indicates that the specified callback should be invoked when instances are activated.
        /// </summary>
        /// <param name="action">The action callback.</param>
        /// <returns>The fluent syntax to define more information</returns>
        public IFilterBindingOnSyntax<T> OnActivation(Action<IContext, ControllerContext, ActionDescriptor, T> action)
        {
            this.OnActivation((ctx, instance) =>
                {
                    var parameter = GetFilterContextParameter(ctx);
                    action(ctx, parameter.ControllerContext, parameter.ActionDescriptor, instance);
                });
            return this;
        }

        /// <summary>
        /// Gets the filter context parameter.
        /// </summary>
        /// <param name="ctx">The context.</param>
        /// <returns>The filter context parameter from the context parameters.</returns>
        private static FilterContextParameter GetFilterContextParameter(IContext ctx)
        {
            return (FilterContextParameter)ctx.Parameters.Single(p => p is FilterContextParameter);
        }
    }
}