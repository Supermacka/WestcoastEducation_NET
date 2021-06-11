namespace Api.DTOs
{
    public class CourseUpdate
    {
        public string CourseNumber {get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public string Difficulty { get; set; }
        public string Status { get; set; }
    }
}