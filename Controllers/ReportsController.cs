using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.Services;
using SchoolAdministrationSystem.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace SchoolAdministrationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;

        public ReportsController(IStudentService studentService, IClassService classService)
        {
            _studentService = studentService;
            _classService = classService;
        }

        // GET: Reports/Create
        public async Task<IActionResult> Create()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var classes = await _classService.GetAllClassesAsync();

            ViewBag.Students = students != null
                ? new SelectList(students, "Id", "FullName")
                : new SelectList(new List<SelectListItem>());

            ViewBag.Classes = classes != null
                ? new SelectList(classes, "Id", "Speciality")
                : new SelectList(new List<SelectListItem>());

            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportType,StudentId,ClassId,StartDate,EndDate,Id")] ReportDTO reportRequestDTO)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(reportRequestDTO);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var students = await _studentService.GetAllStudentsAsync();
            var classes = await _classService.GetAllClassesAsync();
            ViewBag.StudentId = new SelectList(students, "Id", "FullName");
            ViewBag.ClassId = new SelectList(classes, "Id", "Name");
            return View(reportRequestDTO);
        }

    }
}
