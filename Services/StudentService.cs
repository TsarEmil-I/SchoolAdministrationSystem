using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public StudentService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDTO>> GetAllStudentsByClassIdAsync(int classId)
    {
        var students = await _context.Students
            .Where(s => s.ClassId == classId)
            .Include(s => s.Class)
            .ToListAsync();
        return _mapper.Map<IEnumerable<StudentDTO>>(students);
    }

    public async Task<List<StudentDTO>> GetAllStudentsAsync()
    {
        var students = await _context.Students.ToListAsync();
        return _mapper.Map<List<StudentDTO>>(students);
    }

    public async Task<StudentDTO> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.Class)
            .FirstOrDefaultAsync(s => s.Id == id);
        return student == null ? null : _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> CreateStudentAsync(StudentDTO studentDto)
    {
        var studentEntity = _mapper.Map<Student>(studentDto);

        studentEntity.Class = await _context.Classes.FindAsync(studentDto.ClassId);

        if (studentEntity.Class == null)
        {
            throw new Exception("Invalid class selected.");
        }

        _context.Students.Add(studentEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<StudentDTO>(studentEntity);
    }

    public async Task<StudentDTO> UpdateStudentAsync(int id, StudentDTO studentDto)
    {
        var existingStudent = await _context.Students.FindAsync(id);
        if (existingStudent == null)
        {
            return null;
        }

        _mapper.Map(studentDto, existingStudent);
        _context.Students.Update(existingStudent);
        await _context.SaveChangesAsync();

        return _mapper.Map<StudentDTO>(existingStudent);
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
