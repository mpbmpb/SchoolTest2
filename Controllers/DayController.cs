using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolTest2.Data;
using SchoolTest2.Models;
using SchoolTest2.ViewModels;

namespace SchoolTest2.Controllers
{
    public class DayController : Controller
    {
        private readonly SchoolContext _context;
        private readonly DbHandler _db;

        public DayController(SchoolContext context)
        {
            _context = context;
            _db = new DbHandler(context);
        }

        // GET: Day
        public async Task<IActionResult> Index()
        { 
            return View(await _db.GetAllDaysAsync());
        }

        // GET: Day/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _db.GetDayIncludeSubjectsAsync(id);
            if (day == null)
            {
                return NotFound();
            }
            
            return View(day);
        }

        // GET: Day/Create
        public async Task<IActionResult> Create()
        {
            var subjects = await _db.GetAllSubjectsAsync();

            return View(new DayViewModel(subjects));
        }

        // POST: Day/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DayViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                await _db.AddDayAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Day/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _db.GetDayIncludeSubjectsAsync(id);

            if (day == null)
            {
                return NotFound();
            }
            var subjects = await _db.GetAllSubjectsAsync();

            return View(new DayViewModel(day, subjects));
        }

        // POST: Day/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DayViewModel model)
        {
            int dayId = model.Day.DayId;

            if (id != dayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool succes = await _db.UpdateDayAsync(model);
                if (succes) { return RedirectToAction(nameof(Index)); }
                return NotFound();
            }
            return View(model);
        }

        // GET: Day/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _db.GetDayAsync((int)id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // POST: Day/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var day = await _context.Days.FindAsync(id);
            
            await _db.RemoveAsync(day);
            return RedirectToAction(nameof(Index));
        }
    }
}
