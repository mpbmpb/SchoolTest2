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
    public class CourseDesignController : Controller
    {
        private readonly SchoolContext _context;
        private readonly DbHandler _db;

        public CourseDesignController(SchoolContext context)
        {
            _context = context;
            _db = new DbHandler(context);
        }

        // GET: CourseDesign
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseDesign.ToListAsync());
        }

        // GET: CourseDesign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDesign = await _context.CourseDesign
                .FirstOrDefaultAsync(m => m.CourseDesignId == id);
            if (courseDesign == null)
            {
                return NotFound();
            }

            return View(courseDesign);
        }

        // GET: CourseDesign/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseDesign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseDesignId,Name,Description")] CourseDesign courseDesign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseDesign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseDesign);
        }

        // GET: CourseDesign/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDesign = await _context.CourseDesign.FindAsync(id);
            if (courseDesign == null)
            {
                return NotFound();
            }
            return View(courseDesign);
        }

        // POST: CourseDesign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseDesignId,Name,Description")] CourseDesign courseDesign)
        {
            if (id != courseDesign.CourseDesignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseDesign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseDesignExists(courseDesign.CourseDesignId))
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
            return View(courseDesign);
        }

        // GET: CourseDesign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseDesign = await _context.CourseDesign
                .FirstOrDefaultAsync(m => m.CourseDesignId == id);
            if (courseDesign == null)
            {
                return NotFound();
            }

            return View(courseDesign);
        }

        // POST: CourseDesign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseDesign = await _context.CourseDesign.FindAsync(id);
            _context.CourseDesign.Remove(courseDesign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseDesignExists(int id)
        {
            return _context.CourseDesign.Any(e => e.CourseDesignId == id);
        }
    }
}
