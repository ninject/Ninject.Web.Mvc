namespace Ninject.Web.Mvc
{
    using System.Web;
    using System.Web.Routing;
    using Ninject.Modules;
    using Ninject.Web.Common;

    public class MvcModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectMvcHttpApplicationPlugin>();

            this.Bind<RouteCollection>().ToConstant(RouteTable.Routes);
            this.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
        }
    }
}