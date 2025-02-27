using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Models;
using System.Threading.Tasks;

namespace SchoolAdministrationSystem.Controllers
{
    public class TeachersController : Controller
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return View(teachers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound();

            return View(teacher);
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
        public async Task<IActionResult> Create(TeacherDTO teacherDto)
        {
            if (!ModelState.IsValid) return View(teacherDto);

            await _teacherService.CreateTeacherAsync(teacherDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherDTO teacherDto)
        {
            if (!ModelState.IsValid) return View(teacherDto);

            await _teacherService.UpdateTeacherAsync(id, teacherDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
