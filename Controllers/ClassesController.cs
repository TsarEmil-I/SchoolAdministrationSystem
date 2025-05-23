﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Models;
using SchoolAdministrationSystem.Services;
using System.Threading.Tasks;

namespace SchoolAdministrationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassesController : Controller
    {
        private readonly IClassService _classService;
        private readonly ITeacherService _teacherService;
        public ClassesController(IClassService classService, ITeacherService teacherService)
        {
            _classService = classService;
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _classService.GetAllClassesAsync();
            return View(classes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var classDetails = await _classService.GetClassByIdAsync(id);
            if (classDetails == null) return NotFound();

            return View(classDetails);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.TeacherId = (await _teacherService.GetAllTeachersWithoutClassesAsync())
                .Select(t => new SelectListItem() { Text = t.FullName, Value = t.Id.ToString() })
                .ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassDTO classDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TeacherId = (await _teacherService.GetAllTeachersWithoutClassesAsync())
                    .Select(t => new SelectListItem() { Text = t.FullName, Value = t.Id.ToString() })
                    .ToList();
                return View(classDto);
            }

            try
            {
                await _classService.CreateClassAsync(classDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException m)
            {
                ModelState.AddModelError("", m.Message);
                return View(classDto);
            }

            await _classService.CreateClassAsync(classDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var classDetails = await _classService.GetClassByIdAsync(id);
            if (classDetails == null) return NotFound();

            ViewBag.TeacherId = (await _teacherService.GetAllTeachersWithoutClassesAsync())
                .Select(t => new SelectListItem() { Text = t.FullName, Value = t.Id.ToString() })
                .ToList();

            return View(classDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassDTO classDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TeacherId = (await _teacherService.GetAllTeachersWithoutClassesAsync())
                    .Select(t => new SelectListItem() { Text = t.FullName, Value = t.Id.ToString() })
                    .ToList();
                return View(classDto);
            }
            await _classService.UpdateClassAsync(id, classDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var classDetails = await _classService.GetClassByIdAsync(id);
            if (classDetails == null) return NotFound();

            return View(classDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _classService.DeleteClassAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
