﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MastiTimes.Models;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MastiTimes.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            News news = new News();
            var bollywood = news.getBollywoodNews();

            //filter top 5 articles and pass them to viewbag
            List<Articles> TopFive = new List<Articles>();
            for (int i = 0; i < 5; i++)
            {
                Articles article = new Articles();
                article.title = bollywood[i].title;
                article.url = bollywood[i].url;
                article.urlToImage = bollywood[i].urlToImage;
                article.description = bollywood[i].description;
                TopFive.Add(article);
            }
            ViewBag.Articles = TopFive;

            RootMovies mov = new RootMovies();
            var result = mov.getUpcomingMovies();
            return View(result);
        }

        public IActionResult News()
        {
            News news = new News();
            RootMovies movies = new RootMovies();
            var result = news.getBollywoodNews();
            var hollywood = news.getHollywoodNews();
            ViewBag.hollywood = hollywood;
            ViewBag.trailers = movies.GetNowPlayingTrailers();
            return View(result);
        }

        public IActionResult Test(string s)
        {
            if(s == "null")
            {
                ViewBag.LoginError = "Please login to view the page.";
            }          
            return View();
        }

        public IActionResult LikeTheater(string howdy)
        {
            User LoggedIn = null;
            if (LoggedIn == null)
            {
                return Json(new { Message = "No", JsonRequestBehavior.AllowGet });
            }
            //else { string message = "SUCCESS"; }
            return RedirectToAction("Test");


        }

        [System.Web.Mvc.HttpPost]
        public IActionResult SearchByText(string searchText)
        {
            RootMovies mov = new RootMovies();
            var result = mov.getMoviesBySearchText(searchText);
            
            return View(result);
        }

        public IActionResult GetSelectedMovie(int id)
        {
            RootMovies mov = new RootMovies();
            Search result = mov.getSelectedMovie(id);
            return View(result);
        }       



    }
}
