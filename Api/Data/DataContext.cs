using System;
using System.Threading.Tasks;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Course> Courses {get; set;}
        public DbSet<Student> Students { get; set; }
        public DataContext(DbContextOptions options) : base(options) {}

        internal Task SingleOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}