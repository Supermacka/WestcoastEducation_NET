using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CourseDto
    {
        public string CourseNumber {get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Range(1, 500, ErrorMessage = "Length must be between 1 and 500 weeks")]
        public int Length { get; set; }
        
        [RegularExpression("Beginner|Intermediate|Advanced", ErrorMessage = "Difficulty can only be <Beginner>, <Intermediate> or <Advanced> ")]
        public string Difficulty { get; set; }

        [RegularExpression("Active|Retired", ErrorMessage = "Status can only be <Active> or <Retired> ")]
        public string Status { get; set; }
    }
}