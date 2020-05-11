using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MastiTimes.Data;
using MastiTimes.Models;

namespace MastiTimes.Controllers
{
    public class MovieController : Controller
    {
        private readonly MastiTimesContext _context;

        public MovieController(MastiTimesContext context)
        {
            _context = context;
        }

        // GET: Movie
        public  IActionResult Index()
        {
            //Gets error message to display from Create method 
            var a = TempData["MovieAdd"];
            if (a != null)
                ViewData["MovieAdd"] = a;
            //Gets error message to display from Delete method 
            var d = TempData["MovieDelete"];
            if (d != null)
                ViewData["MovieDelete"] = d;

            var movies =  DAL.GetMovies();
            return View(movies);
        }

        public IActionResult ShowTimes()
        {
            Theater theater = new Theater();
            List<Theater> theaters = new List<Theater>();

            theaters = theater.GetTheatersByCity("Kathmandu");
            return View(theaters);
        }

        #region Details method
        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        #endregion

        #region create method
        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Title,DateReleased,PosterUrl,Actors,Likes,Rated,Votes,Rating,Country,Language,Trailer,Duration,Genre,ID")] Movie movie)
        {
            try
            {
                int m = DAL.AddMovie(movie);
                if (m == -1)
                {
                    TempData["MovieAdd"] = "Error occured when adding the movie!";
                }
                else
                {
                    TempData["MovieAdd"] = "Successfully added the movie!";
                }

            }
            catch
            {
                TempData["MovieAdd"] = "Error occured when adding the movie!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edit method
        // GET: Movie/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = DAL.GetMovieByID(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Title,DateReleased,PosterUrl,Actors,Likes,Rated,Votes,Rating,Country,Language,Trailer,Duration,Genre,ID")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            try
            {
                int m = DAL.EditMovie(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete method
        // GET: Movie/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = DAL.GetMovieByID(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = DAL.DeleteMovie(id);
            if(movie == -1)
            {
                TempData["MovieDelete"] = "Error occured when deleting the movie!";
            }
            else
            {
                TempData["MovieDelete"] = "Successfully deleted the movie!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }


    }
}
