using System;
using System.Collections.Generic;
using System.Linq;
using SchoolTest2.Models;

namespace SchoolTest2.ViewModels
{
    public class DayViewModel
    {
        public Day Day { get; set; }
        public List<CheckedId> CheckList { get; set; }

        public DayViewModel()
        {
        }

        public DayViewModel(List<Subject> subjects)
        {
            Day = new Day();
            CheckList = new List<CheckedId>();

            foreach (var subject in subjects)
            {
                var check = new CheckedId()
                {
                    Id = subject.SubjectId,
                    Name = subject.Name,
                    IsSelected = false
                };
                CheckList.Add(check);
            }
        }

        public DayViewModel(Day day, List<Subject> subjects)
        {
            Day = day;
            CheckList = new List<CheckedId>();

            foreach (var subject in subjects)
            {
                bool isInDaySubjects = (Day.DaySubjects.Any(x => x.SubjectId == subject.SubjectId));

                var check = new CheckedId()
                {
                    Id = subject.SubjectId,
                    Name = subject.Name,
                    IsSelected = isInDaySubjects
                };

                CheckList.Add(check);
            }
        }
    }
}
