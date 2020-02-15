using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ContentManagementSystem.Live.Models;
using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using System.Data.SqlClient;

namespace ContentManagementSystem.Live.Controllers
{
    public class SubscriptionController : Controller
    {
        private CMSDBEntities context = new CMSDBEntities();
        private readonly ISPRepository _spRepository;
        public SubscriptionController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }

        [HttpGet]
        public ActionResult GetSubscriptionForm()
        {
            try
            {
                return Common.GetSubscriptionForm();
            }
            catch (Exception exp)
            {
                return Common.GetGlobalContentException(exp);
            }
        }

        [HttpPost]
        public ActionResult SaveSubscription(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return Json(new { success = false, errorMessage = "Please enter email before save." }, JsonRequestBehavior.AllowGet);

                ObjectResult result = this._spRepository.GetAll<ObjectResult>("EmailExist @Email", new SqlParameter("Email", email)).Single();

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "You are already registered with us. Thanks" }, JsonRequestBehavior.AllowGet);
                }

                ObjectResult response = this._spRepository.GetAll<ObjectResult>("AddSubscription @Email", new SqlParameter("Email", email)).Single();
                if (response.Succeeded)
                {
                    return Json(new { success = true, message = "Thank you for subscribing to CMS. You will get the notifications for our latest content in your email." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = Common.GetGlobalErrorMessage() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exp)
            {
                return Common.GetGlobalJsonException(exp);
            }
        }
    }
}