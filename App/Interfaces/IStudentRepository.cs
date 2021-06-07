using System.Collections.Generic;
using System.Threading.Tasks;
using App.Entities;

namespace App.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByEmailAsync (string email);
        Task<Student> GetStudentByIdAsync(int id);
        void Update(Student student);
        void Delete(Student student);
    }
}