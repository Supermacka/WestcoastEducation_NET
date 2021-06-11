using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class AddCourseViewModel
    {
        [Display(Name = "Course number")]
        [Required(ErrorMessage = "Course number is missing!")]
        public string CourseNumber {get; set; }
        
        [Required(ErrorMessage = "Title is missing!")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Description is missing!")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Length of course is missing!")]
        [Range(1, 500, ErrorMessage = "The value for Length ")]
        public int Length { get; set; }
        
        [Required(ErrorMessage = "Difficulty of course is missing!")]
        public string Difficulty { get; set; }
        
        [Required(ErrorMessage = "Status of course is missing!")]
        public string Status { get; set; }
    }
}