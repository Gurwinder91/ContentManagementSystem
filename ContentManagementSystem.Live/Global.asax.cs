using ContentManagementSystem.Live.Controllers;
using ContentManagementSystem.Live.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ContentManagementSystem.Live
{
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Bootstrapper.Initialise();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = HttpContext.Current.Server.GetLastError();
            logger.Error(exception);
            RedirectToCustomErrorPages(exception);
        }

        public void RedirectToCustomErrorPages(Exception exception)
        {
            HttpException httpException = new HttpException();

            if (exception is HttpRequestValidationException)
            {
                httpException = new HttpException(9191, "Dangerous Request");
            }
            else
            {
                httpException = exception as HttpException ?? new HttpException(500, "Internal Server Error");
            }

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            switch (httpException.GetHttpCode())
            {
                case 404:
                    routeData.Values.Add("action", "NotFound");
                    break;
                case 9191:
                    routeData.Values.Add("action", "DangerousRequest");
                    break;
                default:
                    routeData.Values.Add("action", "ServerError");
                    break;
            }

            Response.Clear();
            HttpContext.Current.Server.ClearError();

            IController controller = new ErrorController();
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}
