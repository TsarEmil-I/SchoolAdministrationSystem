using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;


public class ClassService : IClassService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClassService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClassDTO>> GetAllClassesAsync()
    {
        var classes = await _context.Classes
            .Include(c => c.Teacher)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ClassDTO>>(classes);
    }

    public async Task<ClassDTO> GetClassByIdAsync(int id)
    {
        var classEntity = await _context.Classes
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == id);

        return classEntity == null ? null : _mapper.Map<ClassDTO>(classEntity);
    }

    public async Task<ClassDTO> CreateClassAsync(ClassDTO classDto)
    {
        var classEntity = _mapper.Map<Class>(classDto);

        classEntity.Teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == classDto.TeacherId);

        if (classEntity.Teacher == null)
        {
            throw new Exception("Invalid teacher selected.");
        }

        _context.Classes.Add(classEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClassDTO>(classEntity);
    }

    public async Task<ClassDTO> UpdateClassAsync(int id, ClassDTO classDto)
    {
        var existingClass = await _context.Classes.FindAsync(id);
        if (existingClass == null)
        {
            return null;
        }

        _mapper.Map(classDto, existingClass);
        _context.Classes.Update(existingClass);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClassDTO>(existingClass);
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
