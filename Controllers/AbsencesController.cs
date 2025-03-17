using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class AbsencesController : Controller
    {
        private readonly AbsenceService _absenceService;
        private readonly ClassService _classService;
        private readonly StudentService _studentService;

        public AbsencesController(AbsenceService absenceService, ClassService classService, StudentService studentService)
        {
            _absenceService = absenceService;
            _classService = classService;
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var absences = await _absenceService.GetAllAbsencesAsync();
            return View(absences);
        }

        public async Task<IActionResult> Details(int id)
        {
            var absence = await _absenceService.GetAbsenceByIdAsync(id);
            if (absence == null) return NotFound();

            return View(absence);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                           .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                           .ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AbsenceDTO absenceDto)
        {
            if(absenceDto.Days > 5)
            {
                ModelState.AddModelError("Days", "Ученикът не може да използва повече от 5 учебни дни наведнъж!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                               .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                               .ToList();
                return View(absenceDto);
            }

            try
            {
                await _absenceService.CreateAbsenceAsync(absenceDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException m)
            {
                ModelState.AddModelError("", m.Message); 
                return View(absenceDto);
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var absence = await _absenceService.GetAbsenceByIdAsync(id);
            if (absence == null) return NotFound();
            ViewBag.ClassId = (await _classService.GetAllClassesAsync())
               .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
               .ToList();
            ViewBag.StudentId = (await _studentService.GetAllStudentsByClassIdAsync(absence.ClassId))
               .Select(s => new SelectListItem() { Text = s.FullName, Value = s.Id.ToString() })
               .ToList();

            return View(absence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AbsenceDTO absenceDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                   .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                   .ToList();
                ViewBag.StudentId = (await _studentService.GetAllStudentsByClassIdAsync(absenceDto.ClassId))
                   .Select(s => new SelectListItem() { Text = s.FullName, Value = s.Id.ToString() })
                   .ToList();

                return View(absenceDto);
            }

            try
            {
                await _absenceService.UpdateAbsenceAsync(id, absenceDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException m)
            {
                ModelState.AddModelError("", m.Message);
                ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                   .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                   .ToList();
                ViewBag.StudentId = (await _studentService.GetAllStudentsByClassIdAsync(absenceDto.ClassId))
                   .Select(s => new SelectListItem() { Text = s.FullName, Value = s.Id.ToString() })
                   .ToList();
                return View(absenceDto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var absence = await _absenceService.GetAbsenceByIdAsync(id);
            if (absence == null) return NotFound();

            return View(absence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _absenceService.DeleteAbsenceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
