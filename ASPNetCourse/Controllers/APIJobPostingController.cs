using ASPNetCourse.Data;
using ASPNetCourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCourse.Controllers
{
	[Route("api/JobPosting")]
	[ApiController]
	public class APIJobPostingController : ControllerBase
	{

		private readonly ApplicationDbContext _context;

		public APIJobPostingController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var allJobPostings = _context.JobPostings.ToArray();

			return Ok(allJobPostings);
		}

		[HttpGet("GetById/{id}")]
		public IActionResult GetById(int id)
		{
			var jobPosting = _context.JobPostings.Find(id);

			if (jobPosting == null)
			{
				return NotFound();
			}

			return Ok(jobPosting);
		}

		[HttpPost("Create")]
		public IActionResult Create(JobPosting jobPosting)
		{
			if (jobPosting.Id != 0)
			{
				return BadRequest("Id must be null");
			}


			if (jobPosting.CompanyImage == null) jobPosting.CompanyImage = new byte[0];
			if (jobPosting.JobTitle == null) jobPosting.JobTitle = "";
			if (jobPosting.JobLocation == null) jobPosting.JobLocation = "";
			if (jobPosting.JobDescription == null) jobPosting.JobDescription = "";
			if (jobPosting.CompanyName == null) jobPosting.CompanyName = "";
			if (jobPosting.ContactPhone == null) jobPosting.ContactPhone = "";
			if (jobPosting.ContactMail == null) jobPosting.ContactMail = "";
			if (jobPosting.ContactWebsite == null) jobPosting.ContactWebsite = "";
			if (jobPosting.OwnerUsername == null) jobPosting.OwnerUsername = "";
			_context.JobPostings.Add(jobPosting);
			_context.SaveChanges();

			return Ok(jobPosting);
		}

		[HttpDelete("Delete/{id}")]
		public IActionResult Delete(int id)
		{
			var jobPosting = _context.JobPostings.Find(id);

			if (jobPosting == null)
			{
				return NotFound();
			}

			_context.JobPostings.Remove(jobPosting);
			_context.SaveChanges();

			return Ok();
		}

		[HttpPut("Update")]
		public IActionResult Update(JobPosting jobPosting)
		{
			if (jobPosting.Id == 0)
			{
				return BadRequest("Id wird benötigt");
			}

			var jobPostingFromDb = _context.JobPostings.Find(jobPosting.Id);

			if (jobPostingFromDb == null)
			{
				return NotFound();
			}

			jobPostingFromDb.CompanyImage = jobPosting.CompanyImage;
			jobPostingFromDb.JobTitle = jobPosting.JobTitle;
			jobPostingFromDb.JobLocation = jobPosting.JobLocation;
			jobPostingFromDb.JobDescription = jobPosting.JobDescription;
			jobPostingFromDb.CompanyName = jobPosting.CompanyName;
			jobPostingFromDb.ContactPhone = jobPosting.ContactPhone;
			jobPostingFromDb.ContactMail = jobPosting.ContactMail;
			jobPostingFromDb.ContactWebsite = jobPosting.ContactWebsite;
			jobPostingFromDb.OwnerUsername = jobPosting.OwnerUsername;

			_context.SaveChanges();

			return Ok(jobPostingFromDb);
		}
	}
}
