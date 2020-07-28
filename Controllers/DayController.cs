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
            // TODO next to refactor
            var day = await _context.Days
                .Where(m => m.DayId == id)
                .Include(d => d.DaySubjects)
                .ThenInclude(ds => ds.Subject)
                .ToListAsync();
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // GET: Day/Create
        public IActionResult Create()
        {
            var subjects = _context.Subjects.ToList();
            var viewModel = new DayViewModel(subjects);

            return View(viewModel);
        }

        // POST: Day/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            var daySubjects = _context.DaySubjects.ToList();

            if (id != dayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Day);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayExists(dayId))
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
                    var existingEntry = daySubjects.FirstOrDefault(
                                            x => x.DayId == dayId && x.SubjectId == item.Id);
                    if (!item.IsSelected && existingEntry != null)
                    {
                        _context.Remove(existingEntry);
                    }

                    if (item.IsSelected && existingEntry == null)
                    {
                        DaySubject ds = new DaySubject()
                        {
                            DayId = dayId,
                            SubjectId = item.Id
                        };
                        _context.Add(ds);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var day = await _context.Days
                .FirstOrDefaultAsync(m => m.DayId == id);
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
            _context.Days.Remove(day);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayExists(int id)
        {
            return _context.Days.Any(e => e.DayId == id);
        }

        //GET
        public IActionResult AddSubject(int? id)
        {
            var day = _context.Days.Single(d => d.DayId == id);
            IEnumerable<Subject> subjects = _context.Subjects.ToList();


            return View(new AddSubjectViewModel(day, subjects));
        }

        //POST
        [HttpPost]
        public IActionResult AddSubject(AddSubjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingDaysubjects = _context.DaySubjects
                .Where(ds => ds.DayId == viewModel.DaySubject.DayId)
                .Where(ds => ds.SubjectId == viewModel.DaySubject.SubjectId).ToList();

                if (existingDaysubjects.Count() == 0)
                {
                    _context.DaySubjects.Add(viewModel.DaySubject);
                    _context.SaveChanges();
                    return Redirect("/Day/Index");
                }
                TempData["ErrorMessage"] = "Subject is already added to Day";
            }
            return RedirectToAction("AddSubject", viewModel);
        }
    }
}
