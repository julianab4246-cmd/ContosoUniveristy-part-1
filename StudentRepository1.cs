using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task AddAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByLastNameAsync(string lastName)
        {
            return await _context.Students
                .Where(s => s.LastName.Contains(lastName))
                .ToListAsync();
        }
    }
}
