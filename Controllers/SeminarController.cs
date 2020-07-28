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
    public class SeminarController : Controller
    {
        private readonly SchoolContext _context;
        private readonly DbHandler _db;

        public SeminarController(SchoolContext context)
        {
            _context = context;
            _db = new DbHandler(context);
        }

        // GET: Seminar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seminars
               .Include(s => s.SeminarDays)
               .ThenInclude(sd => sd.Day)
               .OrderBy(s => s.Name.ToLower())
               .ToListAsync());
        }

        // GET: Seminar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seminar seminar = await _context.Seminars
               .Where(s => s.SeminarId == id)
               .Include(d => d.SeminarDays)
               .ThenInclude(sd => sd.Day)
               .ThenInclude(x => x.DaySubjects)
               .ThenInclude(y => y.Subject)
               .FirstOrDefaultAsync();
            
            if (seminar == null)
            {
                return NotFound();
            }

            return View(seminar);
        }

        // GET: Seminar/Create
        public IActionResult Create()
        {
            var days = _context.Days
                .Include(d => d.DaySubjects)
                .ThenInclude(ds => ds.Subject)
                .ToList();
            var viewModel = new SeminarViewModel(days);

            return View(viewModel);
        }

        // POST: Seminar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeminarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var seminar = new Seminar();
                seminar.Name = model.Seminar.Name;
                seminar.Description = model.Seminar.Description;
                _context.Add(seminar);
                await _context.SaveChangesAsync();

                foreach (var item in model.CheckList)
                {
                    if (item.IsSelected)
                    {
                        SeminarDay sd = new SeminarDay()
                        {
                            DayId = item.Id,
                            SeminarId = seminar.SeminarId
                        };
                        _context.Add(sd);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Seminar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminar = await _context.Seminars
                .Include(s => s.SeminarDays)
                .SingleAsync(x => x.SeminarId == id);
            if (seminar == null)
            {
                return NotFound();
            }
            var days = await _context.Days
                .Include(d => d.DaySubjects)
                .ThenInclude(ds => ds.Subject)
                .ToListAsync();

            return View(new SeminarViewModel(seminar, days));
        }

        // POST: Seminar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SeminarViewModel model)
        {
            int seminarId = model.Seminar.SeminarId;
            var seminarDays = await _context.SeminarDays.ToListAsync();
            if (id != seminarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Seminar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeminarExists(seminarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                foreach (var item in model.CheckList)
                {
                    var existingEntry = seminarDays.FirstOrDefault(
                                            x => x.SeminarId == seminarId && x.DayId == item.Id);
                    if (!item.IsSelected && existingEntry != null)
                    {
                        _context.Remove(existingEntry);
                    }

                    if (item.IsSelected && existingEntry == null)
                    {
                        SeminarDay ss = new SeminarDay()
                        {
                            SeminarId = seminarId,
                            DayId = item.Id
                        };
                        _context.Add(ss);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Seminar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminar = await _context.Seminars
                .FirstOrDefaultAsync(m => m.SeminarId == id);
            if (seminar == null)
            {
                return NotFound();
            }

            return View(seminar);
        }

        // POST: Seminar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminar = await _context.Seminars.FindAsync(id);
            _context.Seminars.Remove(seminar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeminarExists(int id)
        {
            return _context.Seminars.Any(e => e.SeminarId == id);
        }
    }
}
