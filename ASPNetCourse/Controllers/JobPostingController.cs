using ASPNetCourse.Data;
using ASPNetCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCourse.Controllers
{
    [Authorize]
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public JobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var jobPostingsFromDb = _context.JobPostings.Where(x => x.OwnerUsername == User.Identity.Name).ToList();
            return View(jobPostingsFromDb);
        }

        public IActionResult CreateEditJobPosting(int Id)
        {

            if (Id != 0)
            {

                var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == Id);

                if (jobPostingFromDb.OwnerUsername != User.Identity.Name)
                {
                    return Unauthorized();
                }

                if (jobPostingFromDb != null)
                {
                    return View(jobPostingFromDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult DeleteJobPostingById(int Id)
        {
            if (Id == 0)
                return BadRequest();

            var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == Id);

            if (jobPostingFromDb == null) // Korrigierte Bedingung
                return NotFound();

            _context.JobPostings.Remove(jobPostingFromDb);
            _context.SaveChanges();

            return Ok();
        }

        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile companyImage)
        {

            jobPosting.OwnerUsername = User.Identity.Name;

            if (companyImage != null)
            {
                //IFormFile Type zu Byte Array 
                using (var ms = new MemoryStream())
                {
                    companyImage.CopyTo(ms);
                    var bytes = ms.ToArray();
                    jobPosting.CompanyImage = bytes;
                }
            }

            if (jobPosting.Id == 0)
            {
                if (companyImage == null)
                {
                    jobPosting.CompanyImage = new byte[0];
                }
                if (jobPosting.JobTitle == null) jobPosting.JobTitle = "";
                if (jobPosting.JobLocation == null) jobPosting.JobLocation = "";
                if (jobPosting.JobDescription == null) jobPosting.JobDescription = "";
                if (jobPosting.CompanyName == null) jobPosting.CompanyName = "";
                if (jobPosting.ContactPhone == null) jobPosting.ContactPhone = "";
                if (jobPosting.ContactMail == null) jobPosting.ContactMail = "";
                if (jobPosting.ContactWebsite == null) jobPosting.ContactWebsite = "";
                if (jobPosting.OwnerUsername == null) jobPosting.OwnerUsername = "";
                _context.JobPostings.Add(jobPosting); //Posting der Datenbank hinzufügen
            }
            else
            {
                var jobFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == jobPosting.Id); //Das Element wieder geben was die Id hat die ich oben mitgegeben habe (x is basically ein zeiger auf in einem loop)
                if (jobFromDb == null)
                {
                    return NotFound();
                }
                jobFromDb.CompanyImage = jobPosting.CompanyImage;
                jobFromDb.CompanyName = jobPosting.CompanyName;
                jobFromDb.ContactMail = jobPosting.ContactMail;
                jobFromDb.ContactPhone = jobPosting.ContactPhone;
                jobFromDb.ContactWebsite = jobPosting.ContactWebsite;
                jobFromDb.JobDescription = jobPosting.JobDescription;
                jobFromDb.JobLocation = jobPosting.JobLocation;
                jobFromDb.JobTitle = jobPosting.JobTitle;
                jobFromDb.Salary = jobPosting.Salary;
                jobFromDb.StartDate = jobPosting.StartDate;
                jobFromDb.OwnerUsername = jobPosting.OwnerUsername;
                _context.JobPostings.Add(jobPosting);
            }

            _context.SaveChanges(); //Datenbank speichern

            //TODO: write jobposting to db
            return RedirectToAction("Index");
        }
    }
}

