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

            List<String> BooksInPosession = IssueModel.BooksInPosession();

            int count = BooksInPosession.Count();
            count = count / 3;
            if (count > 6)
            {
                @ViewBag.Error = "U heeft al teveel boeken in uw bezit";
                return View();
            }

            if (model.Issue())
                @ViewBag.Error = "Veel leesplezier";
            else
                @ViewBag.Error = "Er is helaas iets fout gegaan, probeer het opnieuw.";

            return View();
        }

        //
        // GET: /Library/Return
        public ActionResult Return()
        {
            if (!UserModel.CurrentUserLoggedIn)
                return RedirectToAction("Index", "Library");

            return View();
        }

        //
        // POST: /Library/Return
        [HttpPost]
        public ActionResult Return(IssueModel model)
        {
            if (!UserModel.CurrentUserLoggedIn)
                return RedirectToAction("Index", "Library");

            if (model.Return())
                @ViewBag.Error = "Dankjewel";
            else
                @ViewBag.Error = "Er is helaas iets fout gegaan, probeer het opnieuw.";

            return View();
        }
    }
}
