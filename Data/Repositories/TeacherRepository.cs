using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;

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
    }
}

