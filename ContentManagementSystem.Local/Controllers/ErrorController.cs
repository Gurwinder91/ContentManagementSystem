using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentManagementSystem.Local.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult DangerousRequest()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }
    }
}