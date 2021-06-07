using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseModel>> GetCoursesAsync();
    }
}