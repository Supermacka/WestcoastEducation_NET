using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task<Course> GetCourseByCourseNumberAync(string courseNumber)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseNumber == courseNumber);
            return course; 
        }

        public async Task<Course> GetCourseByIdAync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}