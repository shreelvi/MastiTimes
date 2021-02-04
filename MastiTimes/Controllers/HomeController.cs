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

        public async Task<IActionResult> Index()
        {
            var watch = Stopwatch.StartNew();

            Theater theater = new Theater();
            List<Theater> theaters = new List<Theater>();

            theaters = theater.GetTheatersByCity("Kathmandu");

            List<MovieTheater> nowMovies = new List<MovieTheater>();
            nowMovies = await DAL.GetNowShowingMovies();

            ViewBag.NowMovies = nowMovies;

            watch.Stop();
            ViewBag.watch = watch.ElapsedMilliseconds;

            return View(theaters);
        }

        public async Task<IActionResult> News()
        {
            News news = new News();
            RootMovies movies = new RootMovies();

            List<Articles> result = await news.getBollyWoodNews();

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
        public IActionResult Comment(string comment, int movie, int user, string title)
        {
            Comment com = new Comment();
            com._MovieID = movie;
            com._UserID = user;
            com._Comment = comment.Trim();


            com._DateCreated = DateTime.Now;

            User LoggedIn = CurrentUser;
            int c = DAL.CommentMovie(com);

                    
            return RedirectToAction("GetSelectedMovieByTitle", new {title = title, movieId = movie });
        }

        public JsonResult LikeTheater(string howdy, int movie, int user)
        {
            Like like = new Like();
            like._MovieID = movie;
            like._UserID = user;
            User LoggedIn = CurrentUser;
            if (LoggedIn.FirstName == "Anony")
            {
                return Json(new { Message = "No", test = 2, JsonRequestBehavior.AllowGet });
            }
            else 
            {
                int check = DAL.LikeMovie(like);
                int likes = DAL.GetMovieLikes(movie);

                return Json(new { Message = "Yes", test = likes, JsonRequestBehavior.AllowGet }); 
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
                int likes = DAL.GetMovieLikes(movieId);

                ViewBag.Likes = likes;
                ViewBag.MovieTimes = showtimes;
                ViewBag.Movie = movieId;

                return View(showtimes);

            }
            else
            {
                RootMovies mov = new RootMovies();
                Search result = mov.getSelectedMovieByTitle(title);

                var showtimes = DAL.GetTimesByMovie(movieId);
                int likes = DAL.GetMovieLikes(movieId);
                var comments = DAL.GetMovieComments(movieId);

                ViewBag.Likes = likes;
                ViewBag.Comments = comments;
                ViewBag.Count = comments.Count();
                ViewBag.MovieTimes = showtimes;
                ViewBag.Movie = movieId;
                if(CurrentUser.ID == 0)
                {
                    ViewBag.User = 11;
                }
                else
                {
                    ViewBag.User = CurrentUser.ID;

                }
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
