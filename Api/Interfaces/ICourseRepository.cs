using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface ICourseRepository
    {
        Task AddAsync(Course course);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByCourseNumberAync(string courseNumber);
        Task<Course> GetCourseByIdAync(int id);
        void Update(Course course);
        Task<bool> SaveAllChanges();
    }
}