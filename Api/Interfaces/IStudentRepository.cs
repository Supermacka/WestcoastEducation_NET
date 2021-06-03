using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student);
        void Update(Student student);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByEmailAsync(string email);
        Task<Student> GetStudentById(int id);
    }
}