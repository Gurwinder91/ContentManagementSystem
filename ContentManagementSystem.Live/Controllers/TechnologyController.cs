using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using ContentManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ContentManagementSystem.Live.Controllers
{
    public class TechnologyController : Controller
    {
        private readonly ISPRepository _spRepository;
        public TechnologyController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }
        public ActionResult GetAllTechnologies()
        {
            IList<Technology_DTO> TechnologyList =  this._spRepository.GetAll<Technology_DTO>("GetAllTechnologies").ToList();          
            return PartialView("~/Views/Technology/_AllTechnologies.cshtml", TechnologyList);
        }


        public ActionResult GetTechnology(string techQueryString)
        {
            Technology_DTO techModel = new Technology_DTO();
            List<Question_DTO> questionList = this._spRepository.GetAll<Question_DTO>("GetQuestionsByTechnology @TechnologyQueryString", new SqlParameter("TechnologyQueryString", techQueryString)).ToList();

            techModel.TechnologyName = questionList.FirstOrDefault().TechnologyName;
            techModel.QuestionList = questionList;
            return View(techModel);

        }
    }
}