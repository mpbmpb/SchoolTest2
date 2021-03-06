﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolTest2.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string RequiredReading { get; set; }
        public ICollection<DaySubject> DaySubjects { get; set; }
    }
}
