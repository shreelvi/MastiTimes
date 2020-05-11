using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MastiTimes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MastiTimes.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Code By: Elvis
        /// Date Created: 03/09/2019
        /// Reference: Prof. PeerVal Project, GitHub
        /// Taken code and modified return view and view data
        /// Modified on: 03/16/2019
        /// --Added view data as URI for the files directory
        /// User can access their directory from the dashboard
        /// </summary>
        public ActionResult Login(string returnUrl)
        {
            var s = TempData["UserAddSuccess"];
            var e = TempData["UserAddError"];


            if (s != null)
                ViewData["UserAddSuccess"] = s;
            else if (e != null)
                ViewData["UserAddError"] = e;

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Created on: 03/07/2019
        /// Created By: Elvis
        /// Attempts to login the user with the provided username and password
        /// Modified On: 03/18/2019
        /// --Return User directory link to the dashboard page
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
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
                
            }
            ViewBag.Error = "Invalid Username and/or Password";
            ViewBag.User = userName;
            return RedirectToAction("Index", "Home");

        }
    }
}