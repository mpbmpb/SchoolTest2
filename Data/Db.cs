﻿using System;
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

        public async Task AddSeminarAsync(SeminarViewModel model)
        {
            _context.Add(model.Seminar);
            await _context.SaveChangesAsync();

            var seminarDays = await _context.SeminarDays.ToListAsync();
            foreach (var item in model.CheckList)
            {
                if (item.IsSelected)
                {
                    SeminarDay sd = new SeminarDay()
                    {
                        SeminarId = model.Seminar.SeminarId,
                        DayId = item.Id
                    };
                    _context.Add(sd);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Day> GetDayAsync(int id)
        {
            return await _context.Days.FindAsync(id);
        }

        public async Task<Day> GetDayIncludeSubjectsAsync(int? id)
        {
            return await _context.Days
                .Include(d => d.DaySubjects)
                .ThenInclude(ds => ds.Subject)
                .SingleAsync(d => d.DayId == id);
        }

        public async Task<List<Day>> GetAllDaysAsync()
        {
            return await _context.Days
                .Include(d => d.DaySubjects)
                .ThenInclude(ds => ds.Subject)
                .OrderBy(d => d.Name.ToLower())
                .ToListAsync();
        }

        public async Task<Seminar> GetSeminarAsync(int id)
        {
            return await _context.Seminars
                .Include(s => s.SeminarDays)
                .SingleAsync(x => x.SeminarId == id);
        }
        
        public async Task<Seminar> GetSeminarIncludingSubjectsAsync(int id)
        {
            return await _context.Seminars
               .Where(s => s.SeminarId == id)
               .Include(d => d.SeminarDays)
               .ThenInclude(sd => sd.Day)
               .ThenInclude(x => x.DaySubjects)
               .ThenInclude(y => y.Subject)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Seminar>> GetAllSeminarsAsync()
        {
            return await _context.Seminars
               .Include(s => s.SeminarDays)
               .ThenInclude(sd => sd.Day)
               .OrderBy(s => s.Name.ToLower())
               .ToListAsync();
        }

        public async Task<Subject> GetSubjectAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
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

        public async Task<List<DaySubject>> GetAllDaySubjectsAsync()
        {
            return await _context.DaySubjects.ToListAsync();
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

        public async Task<bool> UpdateDayAsync(DayViewModel model)
        {
            bool succes = await UpdateAsync(model.Day);
            if (!succes) { return false; }
            var daySubjects = await GetAllDaySubjectsAsync();

            foreach (var item in model.CheckList)
            {
                var existingEntry = daySubjects
                    .FirstOrDefault(x => x.DayId == model.Day.DayId && x.SubjectId == item.Id);
                if (!item.IsSelected && existingEntry != null)
                {
                    await RemoveAsync(existingEntry);
                }

                if (item.IsSelected && existingEntry == null)
                {
                    DaySubject ds = new DaySubject()
                    {
                        DayId = model.Day.DayId,
                        SubjectId = item.Id
                    };
                    _context.Add(ds);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSeminarAsync(SeminarViewModel model)
        {
            bool succes = await UpdateAsync(model.Seminar);
            if (!succes) { return false; }
            var seminarDays = await _context.SeminarDays.ToListAsync();

            foreach (var item in model.CheckList)
            {
                var existingEntry = seminarDays
                    .FirstOrDefault(x => x.SeminarId == model.Seminar.SeminarId && x.DayId == item.Id);
                if (!item.IsSelected && existingEntry != null)
                {
                    await RemoveAsync(existingEntry);
                }

                if (item.IsSelected && existingEntry == null)
                {
                    SeminarDay ds = new SeminarDay()
                    {
                        SeminarId = model.Seminar.SeminarId,
                        DayId = item.Id
                    };
                    _context.Add(ds);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}