using Microsoft.AspNetCore.Mvc;
using StudentJobs.Models;
using System.Diagnostics;
using StudentJobs.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using StudentJobs.Utils;
using System.Security.Claims;

namespace StudentJobs.Controllers
{
    public class HomeController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();

            model.Carousel = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Users).Where(x => x.Users.UserStatus == true).Include(x => x.Sectors).OrderByDescending(x => x.JobPostingCreatedDate).Take(3).ToList();
            model.Sectors = db.Sectors.Where(x => x.SectorStatus == true).Take(8).ToList();
            model.Scorings = db.Scorings.Where(x => x.ScoringStatus == true).Include(x => x.Users).Where(x => x.Users.UserStatus == true).OrderByDescending(x => x.ScoringCreatedDate).Take(6).ToList();
            model.JobPostings = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Users).Where(x => x.Users.UserStatus == true).OrderByDescending(x => x.JobPostingCreatedDate).Take(5).ToList();
            return View(model);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Sector()
        {
            var model = new HomeViewModel();
            model.Sectors = db.Sectors.Where(x => x.SectorStatus == true).ToList();
            return View(model);
        }
        public IActionResult SectorDetails(int id)
        {
            ViewBag.model = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Sectors).Include(x => x.Users).Where(x => x.Sectors.SectorId == id).ToList();
            ViewBag.sector = db.Sectors.Where(x => x.SectorStatus == true).FirstOrDefault(x => x.SectorId == id); ;
            return View();
        }
        public IActionResult JobPostings()
        {
            ViewBag.model = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Sectors).Include(x => x.Users).ToList();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult JobPostingDetails(int id)
        {
            ViewBag.model = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Sectors).Include(x => x.Users).ThenInclude(x => x.Scorings.Where(x => x.ScoringStatus == true)).FirstOrDefault(x => x.JobPostingId == id);
            return View();
        }
        [HttpPost]
        public IActionResult JobPostingDetails(Application model)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;
            if (!int.TryParse(a, out userId))
            {
                return BadRequest("Invalid user ID");
            }
            if (ModelState.IsValid)
            {
                model.UserId = userId;
                model.ApplicationStatus = true;
                model.Cv = model.Cv;
                model.JobPostingId = model.JobPostingId;
                db.Applications.Add(model);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult CompanyDetails(int id)
        {
            ViewBag.model = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Sectors).Include(x => x.Users).ThenInclude(x => x.Scorings.Where(x => x.ScoringStatus == true)).FirstOrDefault(x => x.Users.UserId == id);
            ViewBag.model2 = db.JobPostings.Where(x => x.JobPostingStatus == true).Include(x => x.Sectors).Include(x => x.Users).ThenInclude(x => x.Scorings.Where(x => x.ScoringStatus == true)).Take(5).Where(x => x.Users.UserId == id).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CompanyDetails(Scoring model)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;
            if (!int.TryParse(a, out userId))
            {
                return BadRequest("Invalid user ID");
            }
            if (ModelState.IsValid)
            {
                model.UserId = userId;
                model.ScoringStatus = false;
                model.ScoringTitle = model.ScoringTitle;
                model.ScoringDescription = model.ScoringDescription;
                model.Score = model.Score;
                model.ScoringCreatedDate = DateTime.Now;
                db.Scorings.Add(model);
                db.SaveChanges();
                return Redirect("/Home/Index");
            }
            return View(model);
        }
    }
}