using System;
namespace SchoolTest2.Models
{
    public class DaySubject
    {
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
