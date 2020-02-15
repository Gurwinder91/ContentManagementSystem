using ContentManagementSystem.Model;
using ContentManagementSystem.ViewModel;
using ContentManagementSystem.Live.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ContentManagementSystem.Repository;
using System.Data.SqlClient;

namespace ContentManagementSystem.Live.Controllers
{
    public class BlogController : Controller
    {
        private CMSDBEntities context = new CMSDBEntities();
        private readonly ISPRepository _spRepository;
        public BlogController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }

        public ActionResult SearchParticularQuestion(string question)
        {
            CMSQuestion questionContent = new CMSQuestion();
            Question_DTO obj = new Question_DTO();
            if (string.IsNullOrEmpty(question))
            {
                return View(obj);

            }

             questionContent = context.CMSQuestions.Where(w => w.Question.Contains(question)).FirstOrDefault();
                
            if (questionContent == null)
            {
                context.CMSNotFoundQuestions.Add(new CMSNotFoundQuestion { Question = question });
            }
            else
            {
                questionContent.Count = questionContent.Count + 1;


                obj.Answer = questionContent.Answer;
                obj.PublishedBy = questionContent.PublishedBy;
                obj.Question = questionContent.Question;
                obj.QueryString = questionContent.QueryString;
                obj.AuthorId = questionContent.UploadedBy;
            }
            context.SaveChanges();

            return View(obj);
        }

        public ActionResult GetQuestion(string questionQueryString)
        {
            Question_DTO question = this._spRepository.GetAll<Question_DTO>("GetQuestionByQS @QueryString", new SqlParameter("QueryString", questionQueryString)).Single();
            return View("SearchParticularQuestion", question);
        }

        public ActionResult Search(string question)
        {
            return Json(QuestionListByName(question), JsonRequestBehavior.AllowGet);
        }

        public string[] QuestionListByName(string question)
        {
            return context.CMSQuestions.Where(w => w.Question.Contains(question)).Select(s => s.Question).ToArray();
        }

        public ActionResult GetLatestQuestionsForm(string query)
        {
            try
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                if (String.IsNullOrEmpty(query))
                {
                    selectListItems = this._spRepository.GetAll<SelectListItem>("GetLatestQuestions").ToList();
                }
                else
                {
                    selectListItems = this._spRepository.GetAll<SelectListItem>("GetLatestQuestions @Query", new SqlParameter("Query", query)).ToList();
                }
                return PartialView("~/Views/Blog/_LatestQuestions.cshtml", selectListItems);
            }
            catch (Exception exp)
            {
                return Common.GetGlobalContentException(exp);

            }
        }

        public ActionResult GetPopularQuestionsForm(string query)
        {
            try
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                if (String.IsNullOrEmpty(query))
                {
                    selectListItems = this._spRepository.GetAll<SelectListItem>("GetPopularQuestions").ToList();
                }
                else
                {
                    selectListItems = this._spRepository.GetAll<SelectListItem>("GetPopularQuestions @Query", new SqlParameter("Query", query)).ToList();
                }
                return PartialView("~/Views/Blog/_PopularQuestions.cshtml", selectListItems);
            }
            catch (Exception exp)
            {
                return Common.GetGlobalContentException(exp);

            }
        }

    }
}