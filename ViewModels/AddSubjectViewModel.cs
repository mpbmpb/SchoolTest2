using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTest2.Models;

namespace SchoolTest2.ViewModels
{
    public class AddSubjectViewModel
    {
        public Day Day { get; set; }
        public DaySubject DaySubject { get; set; }
        public List<SelectListItem> Subjects { get; set; }

        public AddSubjectViewModel()
        {
        }

        public AddSubjectViewModel(Day day, IEnumerable<Subject> subjects)
        {
            Subjects = new List<SelectListItem>();

            foreach (var sub in subjects)
            {
                Subjects.Add(new SelectListItem
                {
                    Value = sub.SubjectId.ToString(),
                    Text = sub.Name
                });
            }
            Day = day;
        }
    }
}
