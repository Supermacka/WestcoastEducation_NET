using System.ComponentModel.DataAnnotations;

namespace App.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseNumber {get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Range(0, 1000)]
        public int Length { get; set; }
        
        [RegularExpression("Beginner|Intermediate|Advanced")]
        public string Difficulty { get; set; }

        [RegularExpression("Active|Retired")]
        public string Status { get; set; }
    }
}