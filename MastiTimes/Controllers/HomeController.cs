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
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var watch = Stopwatch.StartNew();
            News news = new News();
            var bollywood =  news.getBollywoodNews();

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

            List<MovieTheater> nowMovies = new List<MovieTheater>();
            nowMovies = DAL.GetNowShowingMovies();

            ViewBag.NowMovies = nowMovies;

            RootMovies mov = new RootMovies();
            var result = mov.getUpcomingMovies();
            watch.Stop();
            ViewBag.watch = watch.ElapsedMilliseconds;
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

        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public IActionResult Test(string comment)
        {
            if(comment == "null")
            {
                ViewBag.LoginError = "Please login to view the page.";
            }          
            return View();
        }

        public JsonResult LikeTheater(string howdy, int movie, int user)
        {
            User LoggedIn = CurrentUser;
            if (LoggedIn == null)
            {
                return Json(new { Message = "No", JsonRequestBehavior.AllowGet });
            }
            else 
            { 
                return Json(new { Message = "Yes", JsonRequestBehavior.AllowGet }); 
            }
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

        public IActionResult GetSelectedMovieByTitle(string title, int movieId, string country)
        {
            if(country == "Nepal")
            {
                Movie movie = new Movie();
                Search nepResult = movie.getNepalMovie(movieId);

                var showtimes = DAL.GetTimesByMovie(movieId);
                ViewBag.MovieTimes = showtimes;
                ViewBag.Movie = movieId;

                return View(showtimes);

            }
            else
            {
                RootMovies mov = new RootMovies();
                Search result = mov.getSelectedMovieByTitle(title);

                var showtimes = DAL.GetTimesByMovie(movieId);
                ViewBag.MovieTimes = showtimes;
                ViewBag.Movie = movieId;

                return View(result);
            }
            
        }

        public IActionResult InsertMovies()
        {
            Movie mov = new Movie();
            bool a = mov.InsertMovie();
            return View(mov);
        }


    }
}
