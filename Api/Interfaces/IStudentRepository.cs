using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;

namespace Api.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student);
        void UpdateAsync(Student student);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentByEmail(string email);
    }
}