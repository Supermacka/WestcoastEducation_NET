using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;
using App.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public async Task<Course> GetCourseByCourseNumberAsync(string courseNumber)
        {
            var banan = _context.Courses.FirstOrDefault(c => c.CourseNumber.Trim().ToLower() == courseNumber.Trim().ToLower());
            return await _context.Courses.FirstOrDefaultAsync(c => c.CourseNumber.Trim().ToLower() == courseNumber.Trim().ToLower());
        }

        public async Task<Course> GetCourseByIdAsync(int id)
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