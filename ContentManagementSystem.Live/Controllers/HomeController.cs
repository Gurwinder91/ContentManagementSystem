using ContentManagementSystem.Model;
using ContentManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagementSystem.Repository;

namespace ContentManagementSystem.Live.Controllers
{

    public class HomeController : Controller
    {
        CMSDBEntities db = new CMSDBEntities();
        private readonly ISPRepository _spRepository;
        public HomeController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }
        public ActionResult Index()
        {

            List<GetQuestionsOfTechnologies_DTO>  questionsTechnologyWise = this._spRepository.GetAll<GetQuestionsOfTechnologies_DTO>("GetQuestionsOfTechnologies").ToList();
            
            List<Technology_DTO> techologiesWithQuestions = (from qs in questionsTechnologyWise
                                                             group qs by qs.TechnologyName into gpqs
                                                              select new Technology_DTO
                                                              {
                                                                  TechnologyName = gpqs.Key,
                                                                  QueryString = null,
                                                                  QuestionList = gpqs.Select(s => new Question_DTO
                                                                  {
                                                                      Id = s.Id,
                                                                      Answer = s.Answer,
                                                                      QueryString = s.QueryString,
                                                                      Question = s.Question,
                                                                      PublishedDate = s.PublishedDate
                                                                  }).ToList()

                                                              }).ToList();

            Technologies_DTO technology = new Technologies_DTO();

            if (techologiesWithQuestions.Count > 0)
            {
                technology.QueryString = null;
                technology.AuthorId = null;
                technology.Technologies = techologiesWithQuestions;
            }

            return View(technology);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}