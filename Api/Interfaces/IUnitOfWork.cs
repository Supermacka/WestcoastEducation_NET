using System.Threading.Tasks;
using Api.Interfaces;

namespace App.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository {get; }
        IStudentRepository StudentRepository {get; }
        Task<bool> Complete();
        bool HasPendingChanges();
    }
}