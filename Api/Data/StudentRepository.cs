using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task<Student> GetStudentByEmailAsync(string email)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.Email == email);
            return student;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.Id == id);
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }
    }
}