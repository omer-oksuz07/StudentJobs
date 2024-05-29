using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentJobs.Models;

namespace StudentJobs.Areas.Management.Controllers
{
    [Area("Management")]
    public class UserController : Controller
    {
        StudentJobsContext db =new StudentJobsContext();
        public IActionResult Index()
        {
            return View();
        }
       
        
    }
}
