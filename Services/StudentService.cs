using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IClassRepository _classRepository;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepository, IClassRepository classRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _classRepository = classRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDTO>> GetAllStudentsByClassIdAsync(int classId)
    {
        var students = await _studentRepository.GetStudentsByClassIdAsync(classId);
        return _mapper.Map<IEnumerable<StudentDTO>>(students);
    }

    public async Task<List<StudentDTO>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllStudentsAsync();
        return _mapper.Map<List<StudentDTO>>(students);
    }

    public async Task<StudentDTO> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        return student == null ? null : _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> CreateStudentAsync(StudentDTO studentDto)
    {
        var studentEntity = _mapper.Map<Student>(studentDto);

        studentEntity.Class = await _classRepository.GetClassByIdAsync(studentDto.ClassId);

        if (studentEntity.Class == null)
        {
            throw new Exception("Избран е невалиден клас.");
        }

        await _studentRepository.CreateStudentAsync(studentEntity);

        return _mapper.Map<StudentDTO>(studentEntity);
    }

    public async Task<StudentDTO> UpdateStudentAsync(int id, StudentDTO studentDto)
    {
        var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
        if (existingStudent == null)
        {
            return null;
        }

        _mapper.Map(studentDto, existingStudent);
        await _studentRepository.UpdateStudentAsync(existingStudent);

        return _mapper.Map<StudentDTO>(existingStudent);
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        return await _studentRepository.DeleteStudentAsync(id);
    }

    public async Task CreateStudentsFromRangeAsync(List<StudentDTO> students)
    {
        await _studentRepository.CreateStudentsFromRangeAsync(_mapper.Map<List<Student>>(students));
    }
}
