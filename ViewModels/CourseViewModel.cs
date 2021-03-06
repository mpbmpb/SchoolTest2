﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolTest2.Models;

namespace SchoolTest2.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public List<CourseDesign> CourseDesigns { get; set; }
        public List<SelectListItem> DesignList { get; set; }
        public List<CourseDate> CourseDates { get; set; }
        public CourseDate CourseDate { get; set; }

        public CourseViewModel()
        {
        }

        public CourseViewModel(List<CourseDesign> designs)
        {
            Course = new Course();
            CourseDesigns = designs;
            DesignList = new List<SelectListItem>();
            DesignList.Add(new SelectListItem { Value = "0", Text = "-- select design --" });

            foreach (var item in designs)
            {
                DesignList.Add(new SelectListItem
                {
                    Value = item.CourseDesignId.ToString(),
                    Text = item.Name
                });

            }
        }

        public CourseViewModel(Course course, List<CourseDesign> designs)
        {
            Course = course;
            CourseDesigns = designs;
            DesignList = new List<SelectListItem>();
            DesignList.Add(new SelectListItem { Value = "0", Text = "-- select design --" });

            foreach (var item in designs)
            {
                DesignList.Add(new SelectListItem
                {
                    Value = item.CourseDesignId.ToString(),
                    Text = item.Name
                });

            }
        }

        public CourseViewModel(Course course)
        {
            Course = course;
            CourseDate = new CourseDate();

        }
    }
}
