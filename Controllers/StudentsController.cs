using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAdministrationSystem.DTOs;

namespace SchoolAdministrationSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ClassService _classService;

        public StudentsController(StudentService studentService, ClassService classService)
        {
            _studentService = studentService;
            _classService = classService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpGet("Students/List")]
        public async Task<IEnumerable<StudentDTO>> GetStudents(int id)
        {
            return await _studentService.GetAllStudentsByClassIdAsync(id);
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

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            ViewBag.ClassId = (await _classService.GetAllClassesAsync())
                .Select(c => new SelectListItem() { Text = c.Speciality, Value = c.Id.ToString(), Selected = (c.Id == student.ClassId) })
                .ToList();
            return View(student);
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
