//-------------------------------------------------------------------------------
// <copyright file="NinjectDependencyResolver.cs" company="bbv Software Services AG">
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

namespace Ninject.Web.Mvc
{
    using System;

    using Ninject.Infrastructure;

    /// <summary>
    /// A basic bootstrapper that can be used to setup web applications.
    /// </summary>
    public interface IBootstrapper : IHaveKernel
    {
        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="createKernelCallback">The create kernel callback function.</param>
        void Initialize(Func<IKernel> createKernelCallback);

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        void ShutDown();
    }
}