using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolTest2.Models
{
    public class CourseDesign
    {
        public int CourseDesignId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CourseDesign> CourseDesigns { get; set; }

    }
}
