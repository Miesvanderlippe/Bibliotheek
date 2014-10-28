using Bibliotheek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibliotheek.Controllers
{
    public class LibraryController : Controller
    {
        //
        // GET: /Library/

        public ActionResult Index(BookModel model)
        {
            return View();
        }
        
        //
        // GET: /Library/Book
        public ActionResult Book(int id)
        {
            @ViewBag.BookID = id.ToString();

            return View();
        }

        //
        // GET: /Library/Book
        public ActionResult Author(int id)
        {
            @ViewBag.AuthorID = id.ToString();

            return View();
        }

        //
        // GET: /Library/Issue
        public ActionResult Issue() {
            if (!UserModel.CurrentUserLoggedIn)
                return RedirectToAction("Index", "Library");

            return View();
        }

        //
        // POST: /Library/Issue
        [HttpPost]
        public ActionResult Issue(IssueModel model)
        {
            if (!UserModel.CurrentUserLoggedIn)
                return RedirectToAction("Index", "Library");

            if (model.Issue())
                @ViewBag.Message = "Veel leesplezier";
            else
                @ViewBag.Message = "Er is helaas iets fout gegaan, probeer het opnieuw.";

            return View();
        }
    }
}
