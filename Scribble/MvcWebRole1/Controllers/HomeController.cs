using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcWebRole1.BusinessLogic;

namespace MvcWebRole1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(string url)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            if (ScribbleWebEndpointResources.UrlFactory.ValidateUrl(url))
            {
                throw new Exception("url is invalid");
            }

            if (!string.IsNullOrEmpty(url))
            {
                //home page. get user input task flow.
                var workTask = await StorageHandler.GetWorkTask(url);
            }
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
