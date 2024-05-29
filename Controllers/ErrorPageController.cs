using Microsoft.AspNetCore.Mvc;

namespace StudentJobs.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
