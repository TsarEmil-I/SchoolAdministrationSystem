using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly ApplicationDbContext _context;

        public AbsenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Absence>> GetAllAbsencesAsync()
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .ToListAsync();
        }

        public async Task<Absence?> GetAbsenceByIdAsync(int id)
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateAbsenceAsync(Absence absence)
        {
            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();

            return absence.Id;
        }

        
        public async Task<bool> UpdateAbsenceAsync(Absence absence)
        {
            _context.Absences.Update(absence);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAbsenceAsync(int id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
                return false;

            _context.Absences.Remove(absence);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
