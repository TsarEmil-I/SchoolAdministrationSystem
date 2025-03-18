using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;

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

        public async Task<Class> UpdateClassAsync(int id, Class classItem)
        {
            var existingClass = await _context.Classes.FindAsync(id);
            if (existingClass == null)
            {
                return null;
            }

            _context.Entry(existingClass).CurrentValues.SetValues(classItem);
            await _context.SaveChangesAsync();

            return existingClass;
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                return false;
            }

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

