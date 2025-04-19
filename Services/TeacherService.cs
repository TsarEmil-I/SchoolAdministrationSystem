using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;
using Microsoft.AspNetCore.Identity;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TeacherService(ITeacherRepository teacherRepository, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<TeacherDTO>> GetAllTeachersAsync()
    {
        var teachers = await _teacherRepository.GetAllTeachersAsync();
        return _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
    }

    public async Task<TeacherDTO> GetTeacherByUserIdAsync(string id)
    {
        var teacher = await _teacherRepository.GetTeacherByUserIdAsync(id);
        return _mapper.Map<TeacherDTO>(teacher);
    }

    public async Task<IEnumerable<TeacherDTO>> GetAllTeachersWithoutClassesAsync()
    {
        var teachers = await _teacherRepository.GetAllTeachersWithoutClassesAsync();
        return _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
    }

    public async Task<TeacherDTO> GetTeacherByIdAsync(int id)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
        return teacher == null ? null : _mapper.Map<TeacherDTO>(teacher);
    }

    public async Task<TeacherDTO> CreateTeacherAsync(TeacherDTO teacherDto)
    {
        var teacherEntity = _mapper.Map<Teacher>(teacherDto);
        var user = new IdentityUser
        {
            UserName = teacherDto.Email,
            Email = teacherDto.Email,
            EmailConfirmed = true
        };

        var userExists = await _userManager.FindByEmailAsync(user.Email);
        if (userExists == null)
        {
            var result = await _userManager.CreateAsync(user, "Teacher@123");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Teacher");
            }
        }

        teacherEntity.UserId = user.Id;

        await _teacherRepository.CreateTeacherAsync(teacherEntity);
        return _mapper.Map<TeacherDTO>(teacherEntity);
    }

    public async Task<TeacherDTO> UpdateTeacherAsync(int id, TeacherDTO teacherDto)
    {
        var existingTeacher = await _teacherRepository.GetTeacherByIdAsync(teacherDto.Id);
        if (existingTeacher == null)
        {
            return null;
        }

        _mapper.Map(teacherDto, existingTeacher);

        await _teacherRepository.UpdateTeacherAsync(existingTeacher);

        return _mapper.Map<TeacherDTO>(existingTeacher);
    }

    public async Task<bool> DeleteTeacherAsync(int id)
    {
        return await _teacherRepository.DeleteTeacherAsync(id);
    }

    public async Task CreateTeachersFromRangeAsync(List<TeacherDTO> teachers)
    {
        await _teacherRepository.CreateTeachersFromRangeAsync(_mapper.Map<List<Teacher>>(teachers));
    }

    public async Task<TeacherDTO> GetTeacherByFullNameAsync(string classTeacher)
    {
        var teacher = await _teacherRepository.GetTeacherByFullNameAsync(classTeacher);
        return _mapper.Map<TeacherDTO>(teacher);
    }
}
