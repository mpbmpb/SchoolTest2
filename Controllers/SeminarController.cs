using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolTest2.Data;
using SchoolTest2.Models;
using SchoolTest2.ViewModels;

namespace SchoolTest2.Controllers
{
    public class SeminarController : Controller
    {
        private readonly DbHandler _db;

        public SeminarController(SchoolContext context)
        {
            _db = new DbHandler(context);
        }

        // GET: Seminar
        public async Task<IActionResult> Index()
        {
            return View(await _db.GetAllSeminarsAsync());
        }

        // GET: Seminar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Seminar seminar = await _db.GetSeminarIncludingSubjectsAsync((int)id);
            
            if (seminar == null)
            {
                return NotFound();
            }
            return View(seminar);
        }

        // GET: Seminar/Create
        public async Task<IActionResult> Create()
        {
            var days = await _db.GetAllDaysAsync();
            return View(new SeminarViewModel(days));
        }

        // POST: Seminar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeminarViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _db.AddSeminarAsync(model);
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

            var seminar = await _db.GetSeminarAsync((int)id);
            if (seminar == null)
            {
                return NotFound();
            }
            var days = await _db.GetAllDaysAsync();

            return View(new SeminarViewModel(seminar, days));
        }

        // POST: Seminar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SeminarViewModel model)
        {
            int seminarId = model.Seminar.SeminarId;
            if (id != seminarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool success = await _db.UpdateSeminarAsync(model);
                if (success) { return RedirectToAction(nameof(Index)); }
                return NotFound();
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

            var seminar = await _db.GetSeminarAsync((int)id);
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
            var seminar = await _db.GetSeminarAsync((int)id);
            await _db.RemoveAsync(seminar);
            return RedirectToAction(nameof(Index));
        }
    }
}
