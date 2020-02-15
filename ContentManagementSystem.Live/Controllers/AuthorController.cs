using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using ContentManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentManagementSystem.Live.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ISPRepository _spRepository;
        public AuthorController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }

        // GET: Author
        public ActionResult GetAll()
        {
            return View();
        }
        public ActionResult GetAuthor(string id)
        {
            Author_DTO author = new Author_DTO();
            try
            {
                if(id == null)
                {
                    author = this._spRepository.GetAll<Author_DTO>("GetAuthorByID @Id", new SqlParameter("Id", id ?? SqlString.Null)).Single();
                    author.AuthorByBlog = false;
                }
                else
                {
                    author = this._spRepository.GetAll<Author_DTO>("GetAuthorByID @Id", new SqlParameter("Id", id ?? SqlString.Null)).Single();
                    author.AuthorByBlog = true;
                }            
            }
            catch(Exception ex)
            {
                author = new Author_DTO();
            }

            return PartialView("~/Views/Author/_AuthorDetails.cshtml", author);
        }
    }
}