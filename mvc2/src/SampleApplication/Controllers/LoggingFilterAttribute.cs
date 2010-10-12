namespace SampleApplication.Controllers
{
    using System.Globalization;
    using System.Web.Mvc;
    using log4net;
    using Ninject;

    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private ILog log;

        [Inject]
        public ILog Log
        {
            get
            {
                return this.log;
            }

            set
            {
                this.log = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Log.Info(string.Format(CultureInfo.InvariantCulture, "Executing: {0}", filterContext.ActionDescriptor.ActionName));
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)  
        {  
            this.Log.Info(string.Format(CultureInfo.InvariantCulture, "Executed: {0}", filterContext.ActionDescriptor.ActionName));
        }
    }
}