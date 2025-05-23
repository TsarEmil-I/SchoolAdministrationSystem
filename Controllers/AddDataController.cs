﻿using AutoMapper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;
using System.Globalization;

[Authorize(Roles = "Admin")]
public class AddDataController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IClassService _classService;
    private readonly IStudentService _studentService;
    private readonly ITeacherService _teacherService;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public AddDataController(ApplicationDbContext context, IMapper mapper, IClassService classService, IStudentService studentService, ITeacherService teacherService, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _classService = classService;
        _studentService = studentService;
        _teacherService = teacherService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ImportStudentsCSV(AddDataDTO model)
    {
        if (model.CsvFile == null || model.CsvFile.Length == 0)
        {
            ModelState.AddModelError("", "Моля, изберете валиден CSV файл.");
            return View("AddData");
        }

        using (var stream = new StreamReader(model.CsvFile.OpenReadStream()))
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            using (var csv = new CsvHelper.CsvReader(stream, csvConfig))
            {
                var records = csv.GetRecords<ImportStudentDTO>().ToList();
                var students = new List<StudentDTO>();
                foreach (var item in records)
                {
                    StudentDTO student = new StudentDTO()
                    {
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        LastName = item.LastName,
                        Gender = item.Gender,
                        Age = item.Age,
                        Address = item.Address,
                        PhoneNumber = item.PhoneNumber,
                        ClassId = (await _classService.GetClassByClassNameAsync(item.ClassName)).Id
                    };

                    students.Add(student);

                }

                await _studentService.CreateStudentsFromRangeAsync(students);
            }
        }
        TempData["SuccessMessage"] = "Импортирането на CSV файла беше успешно!";
        return RedirectToAction("Index", "Students");
    }

    [HttpPost]
    public async Task<IActionResult> ImportTeachersCSV(AddDataDTO model)
    {
        if (model.CsvFile == null || model.CsvFile.Length == 0)
        {
            ModelState.AddModelError("", "Моля, изберете валиден CSV файл.");
            return View("AddData");
        }

        using (var stream = new StreamReader(model.CsvFile.OpenReadStream()))
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            using (var csv = new CsvHelper.CsvReader(stream, csvConfig))
            {
                var records = csv.GetRecords<ImportTeacherDTO>().ToList();
                var teachers = new List<TeacherDTO>();
                foreach (var item in records)
                {
                    var user = new IdentityUser()
                    {
                        UserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true
                    };

                    var userInfo = await _userManager.FindByEmailAsync(user.Email);
                    if (userInfo == null)
                    {
                        var created = await _userManager
                            .CreateAsync(user, "Teacher@123");
                        if (created.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Teacher");
                        }
                    }

                    TeacherDTO teacher = new TeacherDTO()
                    { 
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        LastName = item.LastName,
                        Email = item.Email,
                        UserId = user.Id
                    };

                    teachers.Add(teacher);

                }

                await _teacherService.CreateTeachersFromRangeAsync(teachers);
            }
        }
        TempData["SuccessMessage"] = "Импортирането на CSV файла беше успешно!";
        return RedirectToAction("Index", "Teachers");
    }

    [HttpPost]
    public async Task<IActionResult> ImportClassesCSV(AddDataDTO model)
    {
        if (model.CsvFile == null || model.CsvFile.Length == 0)
        {
            ModelState.AddModelError("", "Моля, изберете валиден CSV файл.");
            return View("AddData");
        }

        using (var stream = new StreamReader(model.CsvFile.OpenReadStream()))
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            using (var csv = new CsvHelper.CsvReader(stream, csvConfig))
            {
                var records = csv.GetRecords<ImportClassDTO>().ToList();
                var classes = new List<ClassDTO>();
                foreach (var item in records)
                {
                    ClassDTO @class = new ClassDTO()
                    {
                       Speciality = item.ClassName,
                       TeacherId = (await _teacherService.GetTeacherByFullNameAsync(item.ClassTeacher)).Id,
                    };

                    classes.Add(@class);
                }

                await _classService.CreateClassesFromRangeAsync(classes);
            }
        }
        TempData["SuccessMessage"] = "Импортирането на CSV файла беше успешно!";
        return RedirectToAction("Index", "Classes");
    }
}
