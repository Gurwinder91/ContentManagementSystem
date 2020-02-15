using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using ContentManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ContentManagementSystem.Local.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private CMSDBEntities context = new CMSDBEntities();

        private readonly ISPRepository _spRepository;
        public QuestionController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }
        public ActionResult Index()
        {
            List<Question_DTO> model = new List<Question_DTO>();
            string loggedUser = User.Identity.GetUserId();

            model = this._spRepository.GetAll<Question_DTO>("GetQuestionsByUser @LoginUser", new SqlParameter("LoginUser", loggedUser)).ToList();
            return View(model);
        }

        public ActionResult Add()
        {
            Question_DTO model = new Question_DTO
            {
                TechnologyList = this._spRepository.GetAll<Technology_DTO>("GetAllTechnologies").ToList(),
                UploadedBy = User.Identity.GetUserName()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Question_DTO model)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] sqlParameters = new SqlParameter[] {
                    new SqlParameter(){ ParameterName = "Id", Value = model.Id},
                    new SqlParameter(){  ParameterName = "Answer", Value = model.Answer},
                    new SqlParameter(){ ParameterName = "Question", Value = model.Question},
                    new SqlParameter(){  ParameterName = "QueryString", Value = model.QueryString},
                    new SqlParameter(){ ParameterName = "TechnologyId", Value = model.TechnologyId},
                    new SqlParameter(){  ParameterName = "UploadedBy", Value =  User.Identity.GetUserId()},

                };
                int result = this._spRepository.GetResponseAsInt<int>("AddOrUpdateQuestion @Id, @Answer, @Question, @QueryString, @TechnologyId, @UploadedBy", sqlParameters);
                return RedirectToAction("Index");
            }

            model.TechnologyList = this._spRepository.GetAll<Technology_DTO>("GetAllTechnologies").ToList();

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question_DTO model = new Question_DTO();
            try
            {
                model = this._spRepository.GetAll<Question_DTO>("GetQuestionByID @Id", new SqlParameter("Id", id)).Single();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error");
            }

            model.TechnologyList = this._spRepository.GetAll<Technology_DTO>("GetAllTechnologies").ToList();
            return View("Add", model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                int result = this._spRepository.GetResponseAsInt<int>("DeleteQuestionByID @Id", new SqlParameter("Id", id));
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error");
            }           
            
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question_DTO model = new Question_DTO();
            try
            {
                model = this._spRepository.GetAll<Question_DTO>("GetQuestionByID @Id", new SqlParameter("Id", id)).Single();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error");

            }

            return View(model);
        }

        public JsonResult ValidateQuestion(string Question)
        {
            int result = this._spRepository.GetResponseAsInt<int>(String.Format("SELECT dbo.QuestionExist('Question', '{0}')", Question));
            if(result == 0)
                return Json(true, JsonRequestBehavior.AllowGet);

            string notificationToUser = String.Format(CultureInfo.InvariantCulture,
                "{0} is already used.", Question);
            return Json(notificationToUser, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateQueryString(string QueryString)
        {
            int result = this._spRepository.GetResponseAsInt<int>(String.Format("SELECT dbo.QuestionExist('QueryString', '{0}')", QueryString));
            if (result == 0)
                return Json(true, JsonRequestBehavior.AllowGet);

            string notificationToUser = String.Format(CultureInfo.InvariantCulture,
                "{0} is already used.", QueryString);
            return Json(notificationToUser, JsonRequestBehavior.AllowGet);
        }
    }
}