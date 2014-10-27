using Bibliotheek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bibliotheek.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {

            if (UserModel.CurrentUserLoggedIn)
            {
                return RedirectToAction("Overview", "Account");
            }

            return View(new LoginModel());
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Login())
                return RedirectToAction("Overview", "Account");
            
            @ViewBag.Error = "Niet succesvol";
            return View(new LoginModel());
        }

        //
        // GET: /Account/Overview
        public ActionResult Overview(UserModel model) {

            if (!UserModel.CurrentUserLoggedIn)
                return RedirectToAction("Login", "Account");

            @ViewBag.Name = model.CurrentUser();

            @ViewBag.Role = "Gebruiker";
            if (UserModel.IsAdmin)
                @ViewBag.Role = "Administrator";
            

            return View();
        }

        //
        // GET: /Account/AddUser
        public ActionResult AddUser() {
            
            if (!UserModel.IsAdmin)
                return RedirectToAction("Index", "Home");
            @ViewBag.MySQLError = "123";
            return View();
        }

        //
        // POST: /Account/AddUser
        [HttpPost]
        public ActionResult AddUser(UserModel model)
        {
            if(!UserModel.IsAdmin)
                return RedirectToAction("Index", "Home");
            
            if(ModelState.IsValid){
                model.AddAccount();
                @ViewBag.ModelStateIsValid = "Ja";
                return View();
            }else{
                @ViewBag.ModelStateIsValid = "Nee";
                return View();
            }
        }

        //
        // GET: /Account/Logout
        public ActionResult Logout() {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}
