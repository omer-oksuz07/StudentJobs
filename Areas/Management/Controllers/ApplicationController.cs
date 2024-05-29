using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentJobs.Models;

namespace StudentJobs.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Company")]
    public class ApplicationController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();

        [HttpGet]
        public IActionResult Index()
        {
            var model = db.Applications.Include(x => x.Users).Include(x => x.JobPostings).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Application? model = db.Applications.Include(x => x.Users).Include(x => x.JobPostings).FirstOrDefault(x => x.ApplicationId == id);
            if (model == null)
            {
                return Redirect("/Management/Application/Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> FalseByJs(int ApplicationId)
        {
            var Application = await db.Applications.FindAsync(ApplicationId);

            if (Application == null)
            {
                return Json("Such a Application Has Not Been Found");
            }

            Application.ApplicationStatus = false;
            await db.SaveChangesAsync();

            return Json("Application Successfully Erased");
        }
        [HttpPost]
        public async Task<JsonResult> TrueByJs(int ApplicationId)
        {
            var Application = await db.Applications.FindAsync(ApplicationId);

            if (Application == null)
            {
                return Json("Such a Application Has Not Been Found");
            }

            Application.ApplicationStatus = true;
            await db.SaveChangesAsync();

            return Json("Application Successfully Activated");
        }
    }
}
