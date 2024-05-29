using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentJobs.Models;
using StudentJobs.Utils;
using System.Security.Claims;

namespace StudentJobs.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Company")]
    public class JobPostingController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        public JobPostingController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnvironment = hostEnviroment;
        }
        [HttpGet]
        public IActionResult Index(string JobPostings)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;
            if (!int.TryParse(a, out userId))
            {
                return BadRequest("Invalid user ID");
            }
            ViewBag.Search = "";
            var model = db.JobPostings.Include(x => x.Users).Include(x => x.Sectors)
                .Where(x => x.JobPostingStatus == true)
                .Where(x => x.UserId == userId)
                .ToList();

            if (!string.IsNullOrEmpty(JobPostings))
            {
                ViewBag.Search = JobPostings;
                model = model
                    .Where(c => c.JobPostingTitle.ToLower().Contains(JobPostings.ToLower()))
                    .ToList();
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.SectorId=new SelectList(db.Sectors.OrderBy(x=>x.SectorFullName).Where(x=>x.SectorStatus==true),"SectorId","SectorFullName",null); 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(JobPosting model, IFormFile? img)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;
            if (!int.TryParse(a, out userId))
            {
                return BadRequest("Invalid user ID");
            }
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.JobPostingImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                else
                {
                    model.JobPostingImageUrl = "/ImageUpload/favicon.jpg";
                }
                model.UserId = userId;
                model.SectorId = model.SectorId;
                model.JobPostingTitle = model.JobPostingTitle;
                model.JobPostingDescription = model.JobPostingDescription;
                model.JobPostingDate = model.JobPostingDate;
                model.JobPostingCreatedDate = DateTime.Now;
                model.JobPostingStatus = true;
                db.JobPostings.Add(model);
                db.SaveChanges();
                return Redirect("/Management/JobPosting/Index");
            }
            ViewBag.SectorId = new SelectList(db.Sectors.OrderBy(x => x.SectorFullName).Where(x => x.SectorStatus == true), "SectorId", "SectorFullName", null);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = db.JobPostings.Find(id);
            if (model == null)
            {
                return Redirect("/Management/JobPosting/Index");
            }
            ViewBag.SectorId = new SelectList(db.Sectors.OrderBy(x => x.SectorFullName).Where(x => x.SectorStatus == true), "SectorId", "SectorFullName", model.SectorId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobPosting model, IFormFile? img)
        {
            var a = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId;
            if (!int.TryParse(a, out userId))
            {
                return BadRequest("Invalid user ID");
            }
            if (ModelState.IsValid)
            {
                var editmodel = db.JobPostings.Find(model.JobPostingId);

                if (editmodel == null)
                {
                    return Redirect("/Management/JobPosting/Index");
                }
                if (img != null)
                {

                    editmodel.JobPostingImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                editmodel.UserId = userId;
                editmodel.SectorId = model.SectorId;
                editmodel.JobPostingTitle = model.JobPostingTitle;
                editmodel.JobPostingDescription = model.JobPostingDescription;
                editmodel.JobPostingDate = model.JobPostingDate;
                editmodel.JobPostingCreatedDate = DateTime.Now;
                editmodel.JobPostingStatus = true;
                await db.SaveChangesAsync();

                return Redirect("/Management/JobPosting/Index");
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            JobPosting? model = db.JobPostings.Include(x => x.Users).FirstOrDefault(x => x.JobPostingId == id);
            if (model == null)
            {
                return Redirect("/Management/JobPosting/Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteByJs(int JobPostingId)
        {
            var JobPostings = await db.JobPostings.FindAsync(JobPostingId);

            if (JobPostings == null)
            {
                return Json("Such a Job Postings Has Not Been Found");
            }

            JobPostings.JobPostingStatus = false;
            await db.SaveChangesAsync();

            return Json("Job Postings Successfully Erased");
        }


    }


}
