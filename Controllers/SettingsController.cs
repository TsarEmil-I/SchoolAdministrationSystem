using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAdministrationSystem.Data.Entities;

namespace SchoolAdministrationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller 
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var holidays = _context.Holidays.ToList();
            return View(holidays);
        }

        [HttpPost]
        public IActionResult AddHoliday(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                _context.Holidays.Add(holiday);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", _context.Holidays.ToList());
        }

        [HttpPost]
        public IActionResult RemoveHoliday(int id)
        {
            var holiday = _context.Holidays.Find(id);
            if (holiday != null)
            {
                _context.Holidays.Remove(holiday);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

