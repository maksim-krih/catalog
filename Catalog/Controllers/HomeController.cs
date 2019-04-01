using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Models;
using Microsoft.AspNetCore.Authorization;
using Catalog.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facilities.ToListAsync());
        }

        // GET: FacilityModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = await _context.Facilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityModel == null)
            {
                return NotFound();
            }

            return View(facilityModel);
        }

        // GET: FacilityModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacilityModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Rating,Phone,FacilityType")] FacilityModel facilityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityModel);
        }

        // GET: FacilityModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = await _context.Facilities.FindAsync(id);
            if (facilityModel == null)
            {
                return NotFound();
            }
            return View(facilityModel);
        }

        // POST: FacilityModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Rating,Phone,FacilityType")] FacilityModel facilityModel)
        {
            if (id != facilityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityModelExists(facilityModel.Id))
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
            return View(facilityModel);
        }

        // GET: FacilityModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = await _context.Facilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityModel == null)
            {
                return NotFound();
            }

            return View(facilityModel);
        }

        // POST: FacilityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilityModel = await _context.Facilities.FindAsync(id);
            _context.Facilities.Remove(facilityModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityModelExists(int id)
        {
            return _context.Facilities.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
