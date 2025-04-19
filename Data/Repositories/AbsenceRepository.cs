using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Utils;

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

        public async Task<List<Absence>> GetAllAbsencesByStudentIdAsync(int studentId)
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<PaginatedListUtil<Absence>> GetPagedAbsencesAsync(int pageNumber, int pageSize)
        {
            var count = await _context.Absences.CountAsync(); 
            var absences = await _context.Absences
                .OrderBy(a => a.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedListUtil<Absence>(absences, count, pageNumber, pageSize);
        }

        public async Task<PaginatedListUtil<Absence>> GetPagedAbsencesByClassIdAsync(int classId, int pageNumber, int pageSize)
        {
            var absences = await _context.Absences
               .Where(item => item.ClassId == classId)
               .OrderBy(a => a.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            var totalAbsences = await _context.Absences.CountAsync();

            return new PaginatedListUtil<Absence>(absences, totalAbsences, pageNumber, pageSize);
        }

        public async Task<List<Absence>> GetAllAbsencesByClassIdAsync(int classId)
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.ClassId == classId)
                .ToListAsync();
        }

        public async Task<List<Absence>> GetAllAbsencesByClassIdPeriodAsync(int classId, DateTime start, DateTime end)
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.ClassId == classId && a.Start <= end && a.End >= start)
                .ToListAsync();
        }

    }
}
