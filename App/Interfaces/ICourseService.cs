using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;
using App.ViewModels;

namespace App.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetCoursesAsync();
        Task<List<CourseModel>> GetSearchCoursesAsync();
        Task<CourseModel> GetCourseAsync(int id);
        Task<CourseModel> GetCourseByCourseNumberAsync(string courseNumber);
        Task<bool> AddCourse(CourseModel model);
        Task<bool> UpdateCourse(string courseNumber, CourseModel model);
        Task<bool> DeleteCourse(int id);
    }
}