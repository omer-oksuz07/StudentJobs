using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentJobs.Models;

namespace StudentJobs.Areas.Management.Controllers
{

    [Area("Management")]
    [Authorize(Roles = "Company")]
    public class ScoringController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();

        [HttpGet]
        public IActionResult Index()
        {
            var model = db.Scorings.Include(x => x.Users).OrderByDescending(x => x.ScoringCreatedDate).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Scoring? model = db.Scorings.Include(x => x.Users).FirstOrDefault(x => x.ScoringId == id);
            if (model == null)
            {
                return Redirect("/Management/Scoring/Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> FalseByJs(int ScoringId)
        {
            var Scoring = await db.Scorings.FindAsync(ScoringId);

            if (Scoring == null)
            {
                return Json("Such a Scoring Has Not Been Found");
            }

            Scoring.ScoringStatus = false;
            await db.SaveChangesAsync();

            return Json("Scoring Successfully Erased");
        }
        [HttpPost]
        public async Task<JsonResult> TrueByJs(int ScoringId)
        {
            var Scoring = await db.Scorings.FindAsync(ScoringId);

            if (Scoring == null)
            {
                return Json("Such a Scoring Has Not Been Found");
            }

            Scoring.ScoringStatus = true;
            await db.SaveChangesAsync();

            return Json("Scoring Successfully Activated");
        }
    }
}
