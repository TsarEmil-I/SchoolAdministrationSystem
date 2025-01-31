﻿using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;
using SchoolAdministrationSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAdministrationSystem.Data.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly ApplicationDbContext _context;

        public AbsenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all absences with related student and class information
        public async Task<List<Absence>> GetAllAbsencesAsync()
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .ToListAsync();
        }

        // Get absence by ID with related student and class information
        public async Task<Absence?> GetAbsenceByIdAsync(int id)
        {
            return await _context.Absences
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        // Create a new absence record
        public async Task<int> CreateAbsenceAsync(Absence absence)
        {
            _context.Absences.Add(absence);
            await _context.SaveChangesAsync();

            return absence.Id;
        }

        // Update an existing absence record
        public async Task<bool> UpdateAbsenceAsync(Absence absence)
        {
            _context.Absences.Update(absence);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an absence record by ID
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
