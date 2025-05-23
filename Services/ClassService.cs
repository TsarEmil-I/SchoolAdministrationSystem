﻿using AutoMapper;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Data.Repositories;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;

public class ClassService : IClassService
{
    private readonly IClassRepository _classRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;

    public ClassService(IClassRepository classRepository, ITeacherRepository teacherRepository, IMapper mapper)
    {
        _classRepository = classRepository;
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClassDTO>> GetAllClassesAsync()
    {
        var classes = await _classRepository.GetAllClassesAsync();
        return _mapper.Map<IEnumerable<ClassDTO>>(classes);
    }

    public async Task<ClassDTO> GetClassByIdAsync(int id)
    {
        var classEntity = await _classRepository.GetClassByIdAsync(id);
        return classEntity == null ? null : _mapper.Map<ClassDTO>(classEntity);
    }

    public async Task<ClassDTO> CreateClassAsync(ClassDTO classDto)
    {
        var classes = await _classRepository.GetAllClassesAsync();
        var classEntity = _mapper.Map<Class>(classDto);
        classEntity.Teacher = await _teacherRepository.GetTeacherByIdAsync(classDto.TeacherId);

        if (classEntity.Teacher == null)
        {
            throw new Exception("Невалиден клас.");
        }

        foreach (var item in classes)
        {
            if (item.Speciality == classEntity.Speciality)
            {
                throw new ArgumentException("Вече съществува такъв клас.");
            }
        }

        var createdClassId = await _classRepository.CreateClassAsync(classEntity);
        classEntity.Id = createdClassId;

        return _mapper.Map<ClassDTO>(classEntity);
    }

    public async Task<ClassDTO> UpdateClassAsync(int id, ClassDTO classDto)
    {
        var classEntity = _mapper.Map<Class>(classDto);
        var updatedClass = await _classRepository.UpdateClassAsync(id, classEntity);
        return updatedClass == null ? null : _mapper.Map<ClassDTO>(updatedClass);
    }


    public async Task<bool> DeleteClassAsync(int id)
    {
        return await _classRepository.DeleteClassAsync(id);
    }

    public async Task<ClassDTO> GetClassByClassNameAsync(string className)
    {
        var entity =  await _classRepository.GetClassByClassName(className);
        return _mapper.Map<ClassDTO>(entity);
    }

    public async Task CreateClassesFromRangeAsync(List<ClassDTO> classes)
    {
        var classEntities =  _mapper.Map<List<Class>>(classes);
        await _classRepository.CreateClassesFromRangeAsync(classEntities);
    }
}
