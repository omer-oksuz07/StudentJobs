using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentJobs.Models;
using StudentJobs.Models.ViewModel;
using System.Security.Claims;

namespace MoonCafe.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Company")]

    public class HomeController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();
        
        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            model.SectorCount = db.Sectors.Count(x => x.SectorStatus == true);
            model.JobPostingsCount = db.JobPostings.Count(x => x.JobPostingStatus == true);
            model.ScoringsCount = db.Scorings.Count(x => x.ScoringStatus == true);
            model.Applications = db.Applications
                                    .Where(x => x.ApplicationStatus == true)
                                    .Include(x => x.JobPostings)
                                    .Include(x => x.Users)
                                    .ToList();
            return View(model);
        }
    }
}