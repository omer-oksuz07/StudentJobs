using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentJobs.Models;
using StudentJobs.Utils;

namespace StudentJobs.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Company")]
    public class SectorController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        public SectorController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnvironment = hostEnviroment;
        }
        [HttpGet]
        public IActionResult Index(string sector)
        {
            ViewBag.Search = "";
            var model = db.Sectors.Where(x => x.SectorStatus == true).ToList();

            if (!string.IsNullOrEmpty(sector))
            {
                ViewBag.Search = sector;
                model = model
                    .Where(c => c.SectorFullName.ToLower().Contains(sector.ToLower()))
                    .ToList();
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Sector? model = db.Sectors.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Sector/Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sector model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                Sector? editmodel = db.Sectors.Find(model.SectorId);

                if (editmodel == null)
                {
                    return Redirect("/Management/Sector/Index");
                }
                if (img != null)
                {
                    
                    editmodel.SectorImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                editmodel.SectorFullName = model.SectorFullName.ToUpper();
                editmodel.SectorStatus = true; // Assuming SectorStatus is a property of Sector
                editmodel.SectorDescription = model.SectorDescription;
                await db.SaveChangesAsync();

                return Redirect("/Management/Sector/Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sector model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.SectorImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                else
                {
                    model.SectorImageUrl = "/ImageUpload/favicon.jpg";
                }
                model.SectorFullName = model.SectorFullName.ToUpper();
                db.Sectors.Add(model);
                model.SectorStatus = true;
                model.SectorDescription = model.SectorDescription;
                db.SaveChanges();
                return Redirect("/Management/Sector/Index");
            }
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Sector? model = db.Sectors.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Sector/Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteByJs(int SectorId)
        {
            var Sector = await db.Sectors.FindAsync(SectorId);

            if (Sector == null)
            {
                return Json("Such a Sector Has Not Been Found");
            }

            Sector.SectorStatus = false;
            await db.SaveChangesAsync();

            return Json("Sector Successfully Erased");
        }
    }
}

