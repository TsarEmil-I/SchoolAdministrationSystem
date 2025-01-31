using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetAllClassesAsync()
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .Include(c => c.Absences)
                .ToListAsync();
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            return await _context.Classes
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .Include(c => c.Absences)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateClassAsync(Class classItem)
        {
            _context.Classes.Add(classItem);
            await _context.SaveChangesAsync();

            return classItem.Id;
        }
    }
}

