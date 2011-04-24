//-------------------------------------------------------------------------------
// <copyright file="NinjectHttpApplication.cs" company="bbv Software Services AG">
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
    using System.Web;
    using Ninject.Infrastructure;

    /// <summary>
    /// Defines an <see cref="HttpApplication"/> that is controlled by a Ninject <see cref="IKernel"/>.
    /// </summary>
    public abstract class NinjectHttpApplication : HttpApplication, IHaveKernel
    {
        /// <summary>
        /// The one per request module to release request scope at the end of the request
        /// </summary>
        private readonly OnePerRequestModule onePerRequestModule;

        private readonly IBootstrapper bootstrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectHttpApplication"/> class.
        /// </summary>
        protected NinjectHttpApplication()
        {
            this.onePerRequestModule = new OnePerRequestModule();
            this.onePerRequestModule.Init(this);
            this.bootstrapper = new Bootstrapper();
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        [Obsolete("Do not use Ninject as Service Locator")]
        public IKernel Kernel
        {
            get
            {
                return this.bootstrapper.Kernel;
            }
        }

        /// <summary>
        /// Executes custom initialization code after all event handler modules have been added.
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.bootstrapper.InitializeHttpApplication(this);
        }
        
        /// <summary>
        /// Starts the application.
        /// </summary>
        public void Application_Start()
        {
            lock (this)
            {
                this.bootstrapper.Initialize(this.CreateKernel);
                this.OnApplicationStarted();
            }
        }

        /// <summary>
        /// Releases the kernel on application end.
        /// </summary>
        public void Application_End()
        {
            lock (this)
            {
                this.bootstrapper.ShutDown();
                this.OnApplicationStopped();
            }
        }

        /// <summary>
        /// Creates the kernel.
        /// </summary>
        /// <returns>The kernel.</returns>
        protected abstract IKernel CreateKernel();

        /// <summary>
        /// Called when the application is started.
        /// </summary>
        protected virtual void OnApplicationStarted()
        {
        }

        /// <summary>
        /// Called when the application is stopped.
        /// </summary>
        protected virtual void OnApplicationStopped()
        {
        }
    }
}