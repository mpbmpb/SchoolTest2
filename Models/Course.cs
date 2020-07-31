using System;
namespace SchoolTest2.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CourseDesign CourseDesign { get; set; }

    }
}
