using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.DTOs.RequestDTOs;
using SchoolAdministrationSystem.Models;
using System.Threading.Tasks;

namespace SchoolAdministrationSystem.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ClassService _classService;
        private readonly TeacherService _teacherService;
        public ClassesController(ClassService classService, TeacherService teacherService)
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
        public async Task<IActionResult> Create(ClassRequestDTO classDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TeacherId = (await _teacherService.GetAllTeachersWithoutClassesAsync())
                    .Select(t => new SelectListItem() { Text = t.FullName, Value = t.Id.ToString() })
                    .ToList();
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
        public async Task<IActionResult> Edit(int id, ClassRequestDTO classDto)
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
