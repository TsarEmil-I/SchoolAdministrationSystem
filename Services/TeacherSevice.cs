using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;

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

    public async Task<IEnumerable<TeacherDTO>> GetAllTeachersAsync()
    {
        var teachers = await _context.Teachers.ToListAsync();
        return _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
    }

    public async Task<IEnumerable<TeacherDTO>> GetAllTeachersWithoutClassesAsync()
    {
        var teachers = await _teacherRepository.GetAllTeachersWithoutClassesAsync();
        return _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
    }

    public async Task<TeacherDTO> GetTeacherByIdAsync(int id)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Id == id);
        return teacher == null ? null : _mapper.Map<TeacherDTO>(teacher);
    }

    public async Task<TeacherDTO> CreateTeacherAsync(TeacherDTO teacherDto)
    {
        var teacherEntity = _mapper.Map<Teacher>(teacherDto);

        _context.Teachers.Add(teacherEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<TeacherDTO>(teacherEntity);
    }

    public async Task<TeacherDTO> UpdateTeacherAsync(int id, TeacherDTO teacherDto)
    {
        var existingTeacher = await _context.Teachers.FindAsync(id);
        if (existingTeacher == null)
        {
            return null;
        }

        _mapper.Map(teacherDto, existingTeacher);
        _context.Teachers.Update(existingTeacher);
        await _context.SaveChangesAsync();

        return _mapper.Map<TeacherDTO>(existingTeacher);
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
