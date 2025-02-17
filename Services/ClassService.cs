using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;
using SchoolAdministrationSystem.Models;
using SchoolAdministrationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ClassService : IClassService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClassService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClassResponseDTO>> GetAllClassesAsync()
    {
        var classes = await _context.Classes
            .Include(c => c.Teacher)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ClassResponseDTO>>(classes);
    }

    public async Task<ClassResponseDTO> GetClassByIdAsync(int id)
    {
        var classEntity = await _context.Classes
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

        return classEntity == null ? null : _mapper.Map<ClassResponseDTO>(classEntity);
    }

    public async Task<ClassResponseDTO> CreateClassAsync(ClassRequestDTO classDto)
    {
        var classEntity = _mapper.Map<Class>(classDto);

        classEntity.Teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == classDto.TeacherId);

        if (classEntity.Teacher == null)
        {
            throw new Exception("Invalid teacher selected.");
        }

        _context.Classes.Add(classEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClassResponseDTO>(classEntity);
    }

    public async Task<ClassResponseDTO> UpdateClassAsync(int id, ClassRequestDTO classDto)
    {
        var existingClass = await _context.Classes.FindAsync(id);
        if (existingClass == null)
        {
            return null;
        }

        _mapper.Map(classDto, existingClass);
        _context.Classes.Update(existingClass);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClassResponseDTO>(existingClass);
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
