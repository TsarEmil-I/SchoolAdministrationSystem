using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers
                .ToListAsync();
        }

        public async Task<List<Teacher>> GetAllTeachersWithoutClassesAsync()
        {
            var teachersWithClass = await _context.Classes
                .Select(item => item.TeacherId)
                .ToListAsync();

            return await _context.Teachers
                .Where(item => !teachersWithClass.Contains(item.Id))
                .ToListAsync();
        }

        public async Task<Teacher> GetTeacherByUserIdAsync(string id)
        {
            var teacher = await _context.Teachers.Where(t => t.UserId == id).FirstOrDefaultAsync();
            return teacher;
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await _context.Teachers
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return teacher.Id;
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task CreateTeachersFromRangeAsync(List<Teacher> teachers)
        {
            await _context.Teachers.AddRangeAsync(teachers);
            await _context.SaveChangesAsync();
        }
    }
}

