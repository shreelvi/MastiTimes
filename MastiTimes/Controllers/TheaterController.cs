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
    public class TheaterController : Controller
    {
        private readonly MastiTimesContext _context;

        public TheaterController(MastiTimesContext context)
        {
            _context = context;
        }

        // GET: Theater
        public IActionResult Index()
        {
            //Gets error message to display from Create method 
            var a = TempData["TheaterAdd"];
            if (a != null)
                ViewData["TheaterAdd"] = a;
            //Gets error message to display from Delete method 
            var d = TempData["TheaterDelete"];
            if (d != null)
                ViewData["TheaterDelete"] = d;

            var theaters = DAL.GetTheaters();
            return View(theaters);
        }

        // GET: Theater/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theater = await _context.Theater
                .FirstOrDefaultAsync(m => m.ID == id);
            if (theater == null)
            {
                return NotFound();
            }

            return View(theater);
        }

        #region create
        // GET: Theater/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Theater/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,City,PhoneNumber,Likes,ID")] Theater theater)
        {
            int m = DAL.AddTheater(theater);
            if (m == -1)
            {
                TempData["TheaterAdd"] = "Error occured when adding the theater!";
            }
            else
            {
                TempData["TheaterAdd"] = "Successfully added the theater!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region edit
        // GET: Theater/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theater = DAL.GetTheaterByID(id);
            if (theater == null)
            {
                return NotFound();
            }
            return View(theater);
        }

        // POST: Theater/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Address,City,PhoneNumber,Likes,ID")] Theater theater)
        {
            if (id != theater.ID)
            {
                return NotFound();
            }

            try
            {
                int m = DAL.EditTheater(theater);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheaterExists(theater.ID))
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

        #region delete

        // GET: Theater/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theater = DAL.GetTheaterByID(id);
            if (theater == null)
            {
                return NotFound();
            }

            return View(theater);
        }

        // POST: Theater/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var theater = DAL.DeleteTheater(id);
            if (theater == -1)
            {
                TempData["TheaterDelete"] = "Error occured when deleting the movie!";
            }
            else
            {
                TempData["TheaterDelete"] = "Successfully deleted the movie!";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool TheaterExists(int id)
        {
            return _context.Theater.Any(e => e.ID == id);
        }
    }
}
