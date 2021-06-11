using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseRepository
    {
        Task AddAsync(Course course);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByCourseNumberAsync(string courseNumber);
        Task<Course> GetCourseByIdAsync(int id);
        void Update(Course course);
        void Delete(Course courseDto);
    }
}