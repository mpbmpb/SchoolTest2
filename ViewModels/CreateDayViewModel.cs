using System;
using System.Collections.Generic;
using SchoolTest2.Models;

namespace SchoolTest2.ViewModels
{
    public class CreateDayViewModel
    {
        public Day Day { get; set; }
        public List<CheckedId> CheckList { get; set; }

        public CreateDayViewModel()
        {
        }

        public CreateDayViewModel(List<Subject> subjects)
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
    }
}
