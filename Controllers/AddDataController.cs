﻿using AutoMapper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IMapper _mapper;

    public AddDataController(ApplicationDbContext context, IMapper mapper, IClassService classService, IStudentService studentService)
    {
        _context = context;
        _mapper = mapper;
        _classService = classService;
        _studentService = studentService;
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
                        ClassId = (await _classService.GetClassByClassName(item.ClassName)).Id
                    };

                    students.Add(student);

                }

                await _studentService.CreateStudentsFromRangeAsync(students);
            }
        }

        return RedirectToAction("Index", "Students");
    }
}
