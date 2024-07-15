using ASPNetCourse.Data;
using ASPNetCourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetCourse.Controllers
{
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

        public IActionResult DeleteJobPosting(int Id)
        {

            if (Id == 0)
                return BadRequest();

            var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == Id);

            if (jobPostingFromDb != null)
                return NotFound();

            _context.JobPostings.Remove(jobPostingFromDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
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

