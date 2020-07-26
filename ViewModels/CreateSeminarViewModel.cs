using System;
using System.Collections.Generic;
using SchoolTest2.Models;

namespace SchoolTest2.ViewModels
{
    public class CreateSeminarViewModel
    {
        public Seminar Seminar { get; set; }
        public List<CheckedId> CheckList { get; set; }

        public CreateSeminarViewModel()
        {
        }

        public CreateSeminarViewModel(List<Day> days)
        {
            Seminar = new Seminar();
            CheckList = new List<CheckedId>();

            foreach (var day in days)
            {
                var check = new CheckedId()
                {
                    Id = day.DayId,
                    Name = day.Name,
                    IsSelected = false
                };
                CheckList.Add(check);
            }
        }
    }
}
