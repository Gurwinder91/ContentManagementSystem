using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContentManagementSystem.Model;
using ContentManagementSystem.ViewModel;
using System.IO;
using Microsoft.AspNet.Identity;
using ContentManagementSystem.Repository;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ContentManagementSystem.Local.Controllers
{

    public class CustomerController : Controller
    {
        private readonly ISPRepository _spRepository;
        public CustomerController(ISPRepository spRepository)
        {
            this._spRepository = spRepository;
        }
        // GET: Customer
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Author_DTO> tblAuthors = this._spRepository.GetAll<Author_DTO>("GetAllAuthors").ToList();        
            return View(tblAuthors);
        }

        // GET: Customer/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author_DTO tblAuthor = new Author_DTO();
            try
            {
                tblAuthor = this._spRepository.GetAll<Author_DTO>("GetAuthorByID @Id", new SqlParameter("Id", id)).Single();
            }
            catch(Exception ex)
            {
                throw new HttpException(500, "Internal Server Error");
            }
           
            if (tblAuthor == null)
            {
                return HttpNotFound();
            }

            return View(tblAuthor);
        }


        // GET: Customer/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            Author_DTO tblAuthor = new Author_DTO();
            try
            {
                if (id == null)
                {
                    string loggedUser = User.Identity.GetUserId();
                    tblAuthor = this._spRepository.GetAll<Author_DTO>("GetAuthorByUserID @UserID", new SqlParameter("UserID", loggedUser)).Single();
                }
                else
                {
                    tblAuthor = this._spRepository.GetAll<Author_DTO>("GetAuthorByID @Id", new SqlParameter("Id", id)).Single();
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error");
            }

            return View(tblAuthor);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author_DTO model)
        {
            if (ModelState.IsValid)
            {
                byte[] getImageBytes = null;
                if (model.File != null)
                {
                    MemoryStream ms = new MemoryStream();
                    model.File.InputStream.CopyTo(ms);
                    getImageBytes  = ms.GetBuffer();
                }

                try
                {
                    SqlParameter[] sqlParameters = new SqlParameter[] {
                    new SqlParameter("Id", model.Id),
                    new SqlParameter("AuthorName", model.AuthorName ?? SqlString.Null),
                    new SqlParameter("City",model.city ?? SqlString.Null),
                    new SqlParameter("Description", model.Description ?? SqlString.Null),
                    new SqlParameter("DOB", model.DOB ?? SqlDateTime.Null),
                    new SqlParameter("Experience", model.Experience ?? SqlInt32.Null),
                    new SqlParameter("Image", getImageBytes ?? SqlBinary.Null)};

                    int result = this._spRepository.GetResponseAsInt<int>("UpdateAuthorByID @Id, @AuthorName, @City, @Description, @DOB, @Experience, @Image", sqlParameters);
                    if (result == 1)
                        return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    throw new HttpException(500, "Internal Server Error");
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ObjectResult objectResult = this._spRepository.GetAll<ObjectResult>("DeleteAuthorByID @Id", new SqlParameter("Id", id)).Single();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
