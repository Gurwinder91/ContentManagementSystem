using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ContentManagementSystem.Live.Models
{
    public static class Common
    {
        /// <summary>
        /// Get the string of the email subscription form
        /// </summary>
        /// <returns></returns>
        public static string GetSubscriptionStringForm()
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/Views/Subscription/_Subscription.cshtml")))
                return System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Subscription/_Subscription.cshtml"));
            else
            {
                WriteExceptionLog(null, "File doesn't exist");
                return GetGlobalErrorMessage();
            }
        }

        public static ActionResult GetSubscriptionForm()
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/Views/Subscription/_Subscription.cshtml")))
                return new PartialViewResult() { ViewName = "~/Views/Subscription/_Subscription.cshtml" };
            else
            {
                WriteExceptionLog(null, "File doesn't exist");
                return new ContentResult()
                {
                    Content = GetGlobalErrorMessage()
                }; ;
            }
        }
        /// <summary>
        /// Returns the global error message
        /// </summary>
        /// <returns></returns>
        public static string GetGlobalErrorMessage()
        {
            return "An error occurred while processing your request.";
        }

        /// <summary>
        /// Get the Json formatted response with global error message when error occurs.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static JsonResult GetGlobalJsonException(Exception exp)
        {
            WriteExceptionLog(exp);
            return new JsonResult()
            {
                Data = new { success = false, errorMessage = GetGlobalErrorMessage() },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Get the Content response with global error message when error occurs.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static ContentResult GetGlobalContentException(Exception exp)
        {
            WriteExceptionLog(exp);
            return new ContentResult()
            {
                Content = GetGlobalErrorMessage()
            };
        }

        /// <summary>
        /// Writes the exception to the log file.
        /// </summary>
        /// <param name="exp"></param>
        public static void WriteExceptionLog(Exception exp, string message="")
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Logs/log.log"), 
                "Exception Time : " + DateTime.Now.ToString()
                + Environment.NewLine
                + "Exception Message : " + (exp != null ? Convert.ToString(exp) : message)
                + Environment.NewLine
                );
        }
    }
}