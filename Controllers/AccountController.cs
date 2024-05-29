using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudentJobs.Models;
using System.Security.Claims;

namespace StudentJobs.Controllers
{
    public class AccountController : Controller
    {
        StudentJobsContext db = new StudentJobsContext();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(User model)
        {

            var user = db.Users.FirstOrDefault(
            x => x.UserEmail == model.UserEmail
            && x.UserPassword == model.UserPassword
            && x.UserStatus == true);

            if (user != null)
            {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim(ClaimTypes.Name, user.UserFullName),
                    new Claim(ClaimTypes.Actor, user.UserImageUrl ?? ""),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())};

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                };

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                if (user.Role == "Admin" || user.Role == "Company")
                {
                    return RedirectToAction("Index", "Home", new { area = "Management", controller = "Home", action = "Index" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { controller = "Home", action = "Index" });
                }

            }
            ViewBag.Message = "Please check your information";
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> SingUp(User model)
        {
            var user = new User();
            user.UserImageUrl = "/ImageUpload/favicon.jpg";
            user.UserFullName = model.UserFullName.ToUpper();
            user.UserEmail = model.UserEmail;
            user.UserPassword = model.UserPassword;
            user.UserTelephone = model.UserTelephone;
            user.Role = model.Role;
            user.UserCompanyName = model.UserCompanyName;
            user.UserCompanyAddress = model.UserCompanyAddress;
            user.UserStatus = true;
            user.UserBirthDate = model.UserBirthDate;
            user.UserCreatedDate = DateTime.Now;
            db.Users.Add(user);
            int changes = db.SaveChanges();
            if (changes > 0)
            {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim(ClaimTypes.Name, user.UserFullName),
                    new Claim(ClaimTypes.Actor, user.UserImageUrl ?? ""),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())};

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                };

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                if (user.Role == "Admin" || user.Role == "Company")
                {
                    return RedirectToAction("Index", "Home", new { area = "Management", controller = "Home", action = "Index" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { controller = "Home", action = "Index" });
                }
            }
            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}
