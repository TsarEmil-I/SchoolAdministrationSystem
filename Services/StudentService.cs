using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.DTOs.ResponseDTOs;
using SchoolAdministrationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StudentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public StudentService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentResponseDTO>> GetAllStudentsByClassIdAsync(int classId)
    {
        var students = await _context.Students
            .Where(s => s.ClassId == classId)
            .Include(s => s.Class)
            .ToListAsync();
        return _mapper.Map<IEnumerable<StudentResponseDTO>>(students);
    }

    public async Task<List<StudentResponseDTO>> GetAllStudentsAsync()
    {
        var students = await _context.Students.ToListAsync();
        return _mapper.Map<List<StudentResponseDTO>>(students);
    }

    public async Task<StudentResponseDTO> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.Class)
            .FirstOrDefaultAsync(s => s.Id == id);
        return student == null ? null : _mapper.Map<StudentResponseDTO>(student);
    }

    public async Task<StudentResponseDTO> CreateStudentAsync(StudentRequestDTO studentDto)
    {
        var studentEntity = _mapper.Map<Student>(studentDto);

        studentEntity.Class = await _context.Classes.FindAsync(studentDto.ClassId);

        if (studentEntity.Class == null)
        {
            throw new Exception("Invalid class selected.");
        }

        _context.Students.Add(studentEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<StudentResponseDTO>(studentEntity);
    }

    public async Task<StudentResponseDTO> UpdateStudentAsync(int id, StudentRequestDTO studentDto)
    {
        var existingStudent = await _context.Students.FindAsync(id);
        if (existingStudent == null)
        {
            return null;
        }

        _mapper.Map(studentDto, existingStudent);
        _context.Students.Update(existingStudent);
        await _context.SaveChangesAsync();

        return _mapper.Map<StudentResponseDTO>(existingStudent);
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return false;
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
}
