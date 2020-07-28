using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolTest2.Models;
using SchoolTest2.ViewModels;

namespace SchoolTest2.Data
{
    public class DbHandler
    {
        private readonly SchoolContext _context;

        public DbHandler(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Object entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Object entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Object entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true ;
        }

        public async Task<Subject> GetSubjectAsync(int id)
        {
            var sub = await _context.Subjects.FindAsync(id);
            
            return sub;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsIncludeDaysAsync()
        {
            return await _context.Subjects
                .Include(s => s.DaySubjects)
                .ThenInclude(ds => ds.Day)
                .ToListAsync();
        }



        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
    }

}
