using Microsoft.AspNetCore.Mvc;

namespace SchoolAdministrationSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult AddData()
        {
            return View();
        }
    }
}
