using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;
using SchoolAdministrationSystem.Models;
using SchoolAdministrationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TeacherService : ITeacherService
{
    private readonly ApplicationDbContext _context;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public TeacherService(ApplicationDbContext context, ITeacherRepository teacherRepository, IMapper mapper)
    {
        _context = context;
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TeacherResponseDTO>> GetAllTeachersAsync()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return _mapper.Map<IEnumerable<TeacherResponseDTO>>(teachers);
    }

    public async Task<IEnumerable<TeacherResponseDTO>> GetAllTeachersWithoutClassesAsync()
    {
        var teachers = await _teacherRepository.GetAllTeachersWithoutClassesAsync();
        return _mapper.Map<IEnumerable<TeacherResponseDTO>>(teachers);
    }

    public async Task<TeacherResponseDTO> GetTeacherByIdAsync(int id)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == id);
        return teacher == null ? null : _mapper.Map<TeacherResponseDTO>(teacher);
    }

    public async Task<TeacherResponseDTO> CreateTeacherAsync(TeacherRequestDTO teacherDto)
    {
        var teacherEntity = _mapper.Map<Teacher>(teacherDto);

        _context.Teachers.Add(teacherEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TeacherResponseDTO>(teacherEntity);
    }

    public async Task<TeacherResponseDTO> UpdateTeacherAsync(int id, TeacherRequestDTO teacherDto)
    {
        var existingTeacher = await _context.Teachers.FindAsync(id);
        if (existingTeacher == null)
        {
            return null;
        }

        _mapper.Map(teacherDto, existingTeacher);
        _context.Teachers.Update(existingTeacher);
        await _context.SaveChangesAsync();

        return _mapper.Map<TeacherResponseDTO>(existingTeacher);
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
}
