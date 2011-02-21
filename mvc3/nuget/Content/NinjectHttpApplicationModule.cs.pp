namespace $rootnamespace$
{
    using System;
    using System.Web;

    using Ninject;
    using Ninject.Web.Mvc;

    public sealed class NinjectHttpApplicationModule : IHttpModule, IDisposable
    {
        #region Ninject Mvc3 extension bootstrapper (Do not touch this code)
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static bool initialized;
        private static bool kernelDisposed;

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// Do not change this method!
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            lock (bootstrapper)
            {
                if (initialized)
                {
                    return;
                }

                initialized = true;
                bootstrapper.Initialize(CreateKernel);
            }
        }

        /// <summary>
        /// Disposes the <see cref="T:System.Web.HttpApplication"/> instance.
        /// Do not change this method!
        /// </summary>
        public void Dispose()
        {
            lock (bootstrapper)
            {
                if (kernelDisposed)
                {
                    return;
                }

                kernelDisposed = true;
                bootstrapper.ShutDown();
            }
        }
        #endregion

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}