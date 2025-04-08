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
        private readonly IAbsenceService _absenceService;


        public ReportsController(IStudentService studentService, IClassService classService, IAbsenceService absenceService)
        {
            _studentService = studentService;
            _classService = classService;
            _absenceService = absenceService;
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
        public async Task<IActionResult> Create(ReportDTO reportRequestDTO)
        {
            if (ModelState.IsValid)
            {
                if (reportRequestDTO.ReportType == "student")
                {
                    return RedirectToAction("ListByStudent", "Absences", new { studentId = reportRequestDTO.StudentId.Value });
                }

                else if (reportRequestDTO.ReportType == "class")
                {
                    return RedirectToAction("ListByClass", "Absences", new { classId = reportRequestDTO.ClassId.Value });
                }

                else if (reportRequestDTO.ReportType == "classPeriod")
                {
                    return RedirectToAction("ListByClassPeriod", "Absences", new { classId = reportRequestDTO.ClassId.Value, start = reportRequestDTO.StartDate.Value, end = reportRequestDTO.EndDate.Value });
                }
            }

            var students = await _studentService.GetAllStudentsAsync();
            var classes = await _classService.GetAllClassesAsync();
            ViewBag.StudentId = new SelectList(students, "Id", "FullName");
            ViewBag.ClassId = new SelectList(classes, "Id", "Name");
            return View(reportRequestDTO);
        }

    }
}
