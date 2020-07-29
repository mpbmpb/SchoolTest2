using System;
using System.Collections.Generic;
using System.Linq;
using SchoolTest2.Models;


namespace SchoolTest2.ViewModels
{
    public class EditSeminarViewModel
    {
        public Seminar Seminar { get; set; }
        public List<CheckedId> CheckList { get; set; }
        public List<Day> Days { get; set; }

        public EditSeminarViewModel()
        {
        }

        public EditSeminarViewModel(Seminar seminar, List<Day> days)
        {
            Seminar = seminar;
            CheckList = new List<CheckedId>();
            Days = days;

            foreach (var day in days)
            {
                bool isInSeminarDays = (Seminar.SeminarDays.Any(x => x.DayId == day.DayId));

                var check = new CheckedId()
                {
                    Id = day.DayId,
                    Name = day.Name,
                    IsSelected = isInSeminarDays
                };
                CheckList.Add(check);
            }
        }
    }
}
