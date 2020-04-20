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
            Movies mov = new Movies();
            var result = mov.getNowPlayingMovies();
            return View(result);
        }

        public IActionResult News()
        {
            News news = new News();
            var result = news.getBollywoodNews();
            return View(result);
        }

        public IActionResult Test()
        {
            Movies mov = new Movies();
            var result = mov.getNowPlayingMovies();
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
