using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.DTOs;
using SchoolAdministrationSystem.Services;

namespace SchoolAdministrationSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;

        public StudentsController(IStudentService studentService, IClassService classService)
        {
            _studentService = studentService;
            _classService = classService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [Authorize]
        [HttpGet("Students/List")]
        public async Task<IEnumerable<StudentDTO>> GetStudents(int id)
        {
            return await _studentService.GetAllStudentsByClassIdAsync(id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                .ToList();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                    .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString() })
                    .ToList();

                return View(studentDto);
            }

            await _studentService.CreateStudentAsync(studentDto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString(), Selected = (c.Id == student.ClassId) })
                .ToList();
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                    .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString(), Selected = (c.Id == studentDto.ClassId) })
                    .ToList();
                return View(studentDto);
            }

            await _studentService.UpdateStudentAsync(id, studentDto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
