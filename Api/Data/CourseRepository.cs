using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public CourseDto ConvertToCourseDto(Course course)
        {
            var courseDto = new CourseDto
            {
                CourseNumber = course.CourseNumber,
                Title = course.Title,
                Description = course.Description,
                Length = course.Length,
                Difficulty = course.Difficulty,
                Status = course.Status
            };

            return courseDto;
        }
        public Course ConvertToCourse(CourseDto courseDto)
        {
            var Course = new Course
            {
                CourseNumber = courseDto.CourseNumber,
                Title = courseDto.Title,
                Description = courseDto.Description,
                Length = courseDto.Length,
                Difficulty = courseDto.Difficulty,
                Status = courseDto.Status
            };

            return Course;
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task<Course> GetCourseByCourseNumberAsync(string courseNumber)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNumber);

            return course; 
        }
        
        public async Task<Course> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            return course;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();

            return courses;
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }
    }
}