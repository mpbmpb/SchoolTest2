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

        public async Task AddDayAsync(DayViewModel model)
        {
            var day = new Day();
            day.Name = model.Day.Name;
            _context.Add(day);
            await _context.SaveChangesAsync();

            foreach (var item in model.CheckList)
            {
                if (item.IsSelected)
                {
                    DaySubject ds = new DaySubject()
                    {
                        DayId = day.DayId,
                        SubjectId = item.Id
                    };
                    _context.Add(ds);
                }
            }

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
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Day> GetDayAsync(int id)
        {
            return await _context.Days.FindAsync(id);
        }
        
        public async Task<Day> GetDayIncludeSubjectsAsync(int? id)
        {
            return await _context.Days
                .Include(d => d.DaySubjects)
                .SingleAsync(d => d.DayId == id);
        }



        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        
        public async Task<IEnumerable<Day>> GetAllDaysAsync()
        {
            return await _context.Days
                .Include(d => d.DaySubjects)
                .ThenInclude(ds => ds.Subject)
                .OrderBy(d => d.Name.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsIncludeDaysAsync()
        {
            return await _context.Subjects
                .Include(s => s.DaySubjects)
                .ThenInclude(ds => ds.Day)
                .ToListAsync();
        }

    }

}
