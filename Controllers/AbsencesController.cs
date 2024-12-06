using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAdministrationSystem.Data;

namespace SchoolAdministrationSystem.Controllers
{
    public class AbsencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbsencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Absences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Absences.Include(a => a.Class).Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Absences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.Class)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // GET: Absences/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Speciality");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reason,Start,End,StudentId,ClassId")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                absence.Student = await _context.Students.FirstOrDefaultAsync(s => s.Id == absence.StudentId);
                absence.Class = await _context.Classes.FirstOrDefaultAsync(c => c.Id == absence.ClassId);

                if (absence.Student == null || absence.Class == null)
                {
                    ModelState.AddModelError("", "Invalid student or class selected.");
                    return View(absence);
                }

                //absence.Student.LeftAbsenceDays -= absence.Days;
                int absenceDays = absence.Days;

                if (absence.Student.LeftAbsenceDays <= 0 || absence.Student.LeftAbsenceDays < absenceDays)
                {
                    ModelState.AddModelError("", "The student does not have enough remaining absence days.");
                    ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Speciality", absence.ClassId);
                    ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", absence.StudentId);
                    return View(absence);
                }

                absence.SequenceNumber = GenerateSequenceNumber(absence);

                _context.Add(absence);
                await _context.SaveChangesAsync();

                _context.Update(absence.Student);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Speciality", absence.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", absence.StudentId);
            return View(absence);
        }

        // GET: Absences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences.FindAsync(id);
            if (absence == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Speciality", absence.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", absence.StudentId);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SequenceNumber,Reason,Start,End,StudentId,ClassId,Id")] Absence absence)
        {
            if (id != absence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceExists(absence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Speciality", absence.Class.Speciality);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", absence.Student.FullName);
            return View(absence);
        }

        // GET: Absences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absence = await _context.Absences
                .Include(a => a.Class)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absence == null)
            {
                return NotFound();
            }

            return View(absence);
        }

        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var absence = await _context.Absences.FindAsync(id);
            if (absence != null)
            {
                _context.Absences.Remove(absence);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceExists(int id)
        {
            return _context.Absences.Any(e => e.Id == id);
        }

        //private int CalculateLeftAbsenceDays()

        private string GenerateSequenceNumber(Absence absence)
        {
            var classEntity = _context.Classes.FirstOrDefault(c => c.Id == absence.ClassId);
            if (classEntity == null)
            {
                throw new Exception("Invalid class selected");
            }

            var classCode = absence.ClassId;

            var studentId = absence.StudentId.ToString("D2");

            var leftAbsenceDays = absence.Student.LeftAbsenceDays.ToString("D2");

            var currentDate = DateTime.Now;
            var date = $"{currentDate:dd.MM.yy}";

            return $"{classCode}-{studentId}-{leftAbsenceDays}/{date}";
        }
    }
}
