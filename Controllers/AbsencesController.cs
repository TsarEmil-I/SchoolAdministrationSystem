using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.Data.Entities;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolAdministrationSystem.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class AbsencesController : Controller
    {
        private readonly IAbsenceService _absenceService;
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<IdentityUser> _userManager;

        public AbsencesController(IAbsenceService absenceService, IClassService classService, IStudentService studentService, ITeacherService teacherService, UserManager<IdentityUser> userManager)
        {
            _absenceService = absenceService;
            _classService = classService;
            _studentService = studentService;
            _userManager = userManager;
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 15)
        {
            var pagedAbsences = new Utils.PaginatedListUtil<AbsenceDTO>(new List<AbsenceDTO>(), 0, 0, 0);

            if (User.IsInRole("Admin"))
            {
                pagedAbsences = await _absenceService.GetPagedAbsencesAsync(pageNumber, pageSize);
            }

            else
            {
                var user = await _userManager.GetUserAsync(User);
                var teacher = await _teacherService.GetTeacherByUserIdAsync(user.Id);
                pagedAbsences = await _absenceService.GetPagedAbsencesByClassIdAsync(teacher.Class.Id, pageNumber, pageSize);
            }
            return View(pagedAbsences);
        }

        [HttpGet("Absences/ListByStudent/{studentId}")]
        public async Task<IActionResult> ListByStudent(int studentId)
        {

            var absences = await _absenceService.GetAbsencesByStudentIdAsync(studentId);

            if (absences == null || !absences.Any())
            {
                TempData["ErrorMessage"] = "Ученикът няма заявления!";
                return RedirectToAction("Create", "Reports");
            }
            return View(absences);
        }

        [HttpGet("Absences/ListByClass/{classId}")]
        public async Task<IActionResult> ListByClass(int classId)
        {
            var absences = await _absenceService.GetAbsencesByClassIdAsync(classId);
            if (absences == null || !absences.Any())
            {
                TempData["ErrorMessage"] = "Класът няма заявления!";
                return RedirectToAction("Create", "Reports");
            }
            return View(absences);
        }

        [HttpGet("Absences/ListByClassPeriod/{classId}/{start}/{end}")]
        public async Task<IActionResult> ListByClassPeriod(int classId, string start, string end)
        {
            var decodedStart = Uri.UnescapeDataString(start);
            var decodedEnd = Uri.UnescapeDataString(end);
            DateTime.TryParseExact(decodedStart, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime startDate);
            DateTime.TryParseExact(decodedEnd, "MM/dd/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime endDate);
            var absences = await _absenceService.GetAbsencesByClassIdPeriodAsync(classId, startDate, endDate);

            if (absences == null || !absences.Any())
            {
                TempData["ErrorMessage"] = "Класът няма заявления за избрания период!";
                return RedirectToAction("Create", "Reports");
            }

            ViewData["Start"] = startDate.ToString("dd/MM/yyyy");
            ViewData["End"] = endDate.ToString("dd/MM/yyyy");
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

            if (User.IsInRole("Admin"))
            {
                ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                          .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                          .ToList();
            }

            else
            {
                var user = await _userManager.GetUserAsync(User);
                var teacher = await _teacherService.GetTeacherByUserIdAsync(user.Id);

                ViewBag.ClassId = new List<ClassDTO>() { (await _classService.GetClassByIdAsync(teacher.Class.Id)) }
                         .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                         .ToList();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AbsenceDTO absenceDto)
        {

            if (!ModelState.IsValid)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                              .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                              .ToList();
                }

                else
                {
                    var user = await _userManager.GetUserAsync(User);
                    var teacher = await _teacherService.GetTeacherByUserIdAsync(user.Id);

                    ViewBag.ClassId = new List<ClassDTO>() { (await _classService.GetClassByIdAsync(teacher.Class.Id)) }
                             .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                             .ToList();
                }
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
