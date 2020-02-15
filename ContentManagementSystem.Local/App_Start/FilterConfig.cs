using ContentManagementSystem.Local.ExceptionHandler;
using System.Web;
using System.Web.Mvc;

namespace ContentManagementSystem.Local
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionHandlerAttribute());
        }
    }
}
