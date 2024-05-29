using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using StudentJobs.Models;
using StudentJobs.Utils;

namespace StudentJobs.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public MyProfileController(IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult MyProfile(int id)
        {
            User? model = db.Users.Find(id);
            if (model == null)
            {
                return Redirect("/Home/Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> MyProfile(User model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                User? editmodel = db.Users.Find(model.UserId);

                if (img != null)
                {
                    await ImageUploader.DeleteImageAsync(_hostEnvironment, editmodel.UserImageUrl);
                    editmodel.UserImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                editmodel.UserFullName = model.UserFullName;
                editmodel.Role = model.Role;
                editmodel.UserEmail = model.UserEmail;
                editmodel.UserPassword = model.UserPassword;
                editmodel.UserCreatedDate = model.UserCreatedDate;
                editmodel.UserStatus = true;
                editmodel.UserCompanyName = model.UserCompanyName;
                editmodel.UserCompanyAddress = model.UserCompanyAddress;
                editmodel.UserTelephone = model.UserTelephone;
                editmodel.UserBirthDate = model.UserBirthDate;
                editmodel.UserUpdateDate = model.UserUpdateDate;
                await db.SaveChangesAsync();
                await HttpContext.SignOutAsync();
                return Redirect("/Home/Index");
            }
            return View(model);
        }
    }
}
