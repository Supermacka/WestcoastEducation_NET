using System.Collections.Generic;
using App.Models;

namespace App.ViewModels
{
    public class SearchCourseViewModel
    {
        public string CourseNumber { get; set; }
        public List<CourseModel> Courses { get; set; }
    }
}