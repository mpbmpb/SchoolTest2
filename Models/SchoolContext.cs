using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolTest2.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<DaySubject> DaySubjects { get; set; }
        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<SeminarDay> SeminarDays { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DaySubject>()
                .HasKey(d => new { d.DayId, d.SubjectId });

            modelBuilder.Entity<SeminarDay>()
                .HasKey(s => new { s.SeminarId, s.DayId });
        }
        
    }
}
