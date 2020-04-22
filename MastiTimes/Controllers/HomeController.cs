using System;
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
    public class HomeController : Controller
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

            Movies mov = new Movies();
            var result = mov.getUpcomingMovies();
            return View(result);
        }

        public IActionResult News()
        {
            News news = new News();
            Movies movies = new Movies();
            var result = news.getBollywoodNews();
            var hollywood = news.getHollywoodNews();
            ViewBag.hollywood = hollywood;
            ViewBag.trailers = movies.GetNowPlayingTrailers();
            return View(result);
        }

        public IActionResult Test()
        {
            DAL.GetAllUsers();
            Movies mov = new Movies();
            var result = mov.getUpcomingMovies();
            string title = result[0].title;
            return View(result);
        }

        [System.Web.Mvc.HttpPost]
        public IActionResult SearchByText(string searchText)
        {
            Movies mov = new Movies();
            var result = mov.getMoviesBySearchText(searchText);
            
            return View(result);
        }

        public IActionResult GetSelectedMovie(int id)
        {
            Movies mov = new Movies();
            Search result = mov.getSelectedMovie(id);
            return View(result);
        }       



    }
}
