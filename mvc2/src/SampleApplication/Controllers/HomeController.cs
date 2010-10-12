using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleApplication.Controllers
{
    using System.ServiceModel.Syndication;

    [HandleError]
    [LoggingFilter]
    public class HomeController : AsyncController
    {
        private IHomeControllerModel homeControllerModel;

        public HomeController(IHomeControllerModel homeControllerModel)
        {
            this.homeControllerModel = homeControllerModel;
        }

        public void IndexAsync()
        {
        }

        public ActionResult IndexCompleted(IEnumerable<SyndicationItem> items)
        {
            ViewData["Message"] = this.homeControllerModel.WelcomeMessage;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
