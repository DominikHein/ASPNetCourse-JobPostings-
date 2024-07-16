using ASPNetCourse.Data;
using ASPNetCourse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNetCourse.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
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
		public IActionResult GetJobPosting(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			var jobPosting = _context.JobPostings.FirstOrDefault(x => x.Id == id);

			if (jobPosting == null)
			{
				return NotFound();
			}

			return Ok(jobPosting);
		}

		public IActionResult GetJobPostingsPartial(string query)
		{
			List<JobPosting> jobPostings;

			if (string.IsNullOrEmpty(query))
			{
				jobPostings = _context.JobPostings.ToList();
			}
			else
			{
				jobPostings = _context.JobPostings
					.Where(x => x.JobTitle.ToLower().Contains(query.ToLower()))
					.ToList();
			}


			return PartialView("_JobPostingListPartial", jobPostings);
		}

	}
}
