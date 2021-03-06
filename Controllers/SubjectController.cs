using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolTest2.Data;
using SchoolTest2.Models;

namespace SchoolTest2.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SchoolContext _context;
        private readonly DbHandler _db;

        public SubjectController(SchoolContext context)
        {
            _context = context;
            _db = new DbHandler(context);
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            return View(await _db.GetAllSubjectsIncludeDaysAsync());
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _db.GetSubjectAsync((int)id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Name,Description,RequiredReading")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _db.AddAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _db.GetSubjectAsync((int)id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Name,Description,RequiredReading")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool updateSucces = await _db.UpdateAsync(subject);
                if (!updateSucces)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _db.GetSubjectAsync((int)id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _db.GetSubjectAsync(id);
            await _db.RemoveAsync(subject);
            return RedirectToAction(nameof(Index));
        }
    }
}
