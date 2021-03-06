﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace SchoolTest2.Models
{
    public class Day
    {
        public int DayId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DaySubject> DaySubjects { get; set; }
        public ICollection<SeminarDay> SeminarDays { get; set; }

    }
}
