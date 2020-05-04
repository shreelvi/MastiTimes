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
    public class MovieTheaterController : Controller
    {
        private readonly MastiTimesContext _context;

        public MovieTheaterController(MastiTimesContext context)
        {
            _context = context;
        }

        // GET: MovieTheater
        public IActionResult Index()
        {
            //Gets error message to display from Create method 
            var a = TempData["MovieTheaterAdd"];
            if (a != null)
                ViewData["MovieTheaterAdd"] = a;
            //Gets error message to display from Delete method 
            var d = TempData["MovieTheaterDelete"];
            if (d != null)
                ViewData["MovieTheaterDelete"] = d;

            List<MovieTheater> movietheaters = new List<MovieTheater>();
            movietheaters = DAL.GetMovieTimes();
            return View(movietheaters);
        }

       
        // GET: MovieTheater/Create
        public IActionResult Create()
        {
            // Gets Data from Database for the dropdown in create view
            // And insert select item in List
            // Reference: https://www.c-sharpcorner.com/article/binding-dropdown-list-with-database-in-asp-net-core-mvc/

            List<Movie> MovieList = new List<Movie>();
            MovieList = DAL.GetMovies();
            //Inserting Select Item for course in List
            MovieList.Insert(0, new Movie { ID = 0, Title = "Select" });
            ViewBag.Movies = MovieList;

            List<Theater> TheaterList = new List<Theater>();
            TheaterList = DAL.GetTheaters();
            //Inserting Select Item for course in List
            TheaterList.Insert(0, new Theater { ID = 0, Name = "Select", City= "Select"});
            ViewBag.Theaters = TheaterList;

           
            return View();
        }

        // POST: MovieTheater/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MovieID,TheaterID,ShowTime,NowPlaying,ID")] MovieTheater movieTheater)
        {
            //Add the class to the coursesemester table
            int retInt = DAL.AddMovieTheater(movieTheater);

            if (retInt < 0)
            {
                TempData["MovieTheaterAdd"] = "Database problem occured when adding the Courses for Semester";
            }

            //If sucessful, assigns the class to the user that is creating
            else
            {
                TempData["MovieTheaterAdd"] = "Class added but problem occured when assigning user the class.";

            }

            return RedirectToAction(nameof(Index));
        }



        // GET: MovieTheater/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheater = await _context.MovieTheater.FindAsync(id);
            if (movieTheater == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "ID", "ID", movieTheater.MovieID);
            ViewData["TheaterID"] = new SelectList(_context.Theater, "ID", "ID", movieTheater.TheaterID);
            return View(movieTheater);
        }

        // POST: MovieTheater/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,TheaterID,ShowTime,NowPlaying,ID")] MovieTheater movieTheater)
        {
            if (id != movieTheater.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieTheater);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTheaterExists(movieTheater.ID))
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
            ViewData["MovieID"] = new SelectList(_context.Movie, "ID", "ID", movieTheater.MovieID);
            ViewData["TheaterID"] = new SelectList(_context.Theater, "ID", "ID", movieTheater.TheaterID);
            return View(movieTheater);
        }

        // GET: Theater/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var m = DAL.GetMovieTheaterByID(id);
            if (m == null)
            {
                return NotFound();
            }

            return View(m);
        }

        // POST: Theater/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var d = DAL.DeleteMovieTheater(id);
            if (d == -1)
            {
                TempData["MovieTheaterDelete"] = "Error occured when deleting the movie theater!";
            }
            else
            {
                TempData["MovieTheaterDelete"] = "Successfully deleted the movie theater!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTheaterExists(int id)
        {
            return _context.MovieTheater.Any(e => e.ID == id);
        }
    }
}
