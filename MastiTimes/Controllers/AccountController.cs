using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MastiTimes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

namespace MastiTimes.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


    
        public ActionResult Login(string returnUrl)
        {
            var s = TempData["UserAddSuccess"];
            var e = TempData["UserAddError"];


            if (s != null)
                ViewData["UserAddSuccess"] = s;
            else if (e != null)
                ViewData["UserAddError"] = e;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String userName, String passWord)
        {
            User loggedIn = DAL.GetUser(userName, passWord);
            if (loggedIn != null)
            {
                CurrentUser = loggedIn; //Sets the session for user from base controller
                HttpContext.Session.SetString("username", loggedIn.UserName);
                HttpContext.Session.SetInt32("UserID", loggedIn.ID); //Sets userid in the session
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Invalid Username and/or Password";
            ViewBag.User = userName;
            return View();

        }

        // GET: /Account/AddUser
        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(User NewUser)
        {
            int check = DAL.CheckUserExists(NewUser.UserName);
            if (check > 0)
            {
                ViewBag.Error = " Username not Unique! Please enter a new username.";
                return View(); //Redirects to add user page

            }
            else
            {
                try
                {
                    int UserAdd = DAL.AddUser(NewUser);
                    if (UserAdd < 1)
                    {
                        TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                    }
                    else
                    {
                        TempData["UserAddSuccess"] = "Registration successful!";
                    }
                }
                catch
                {
                    TempData["UserAddError"] = "Sorry, unexpected Database Error. Please try again later.";
                }
            }
            return RedirectToAction("Login"); //Directs to Login page after success
        }
    }
}