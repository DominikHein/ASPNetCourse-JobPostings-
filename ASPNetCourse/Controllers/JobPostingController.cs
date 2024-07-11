using Microsoft.AspNetCore.Mvc;

namespace ASPNetCourse.Controllers
{
    public class JobPostingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateEditJobPosting(int Id)
        {
            return View();
        }
    }
}
