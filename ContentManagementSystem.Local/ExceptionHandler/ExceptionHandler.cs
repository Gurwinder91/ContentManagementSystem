using System.Web;
using System.Web.Mvc;

namespace ContentManagementSystem.Local.ExceptionHandler
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
               // if (filterContext.Exception is HttpException)
                //{
                    filterContext.Result = new RedirectResult("/Error/ServerError");
                //}
                logger.Error(filterContext.Exception.Message, filterContext.Exception);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}