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
            return View();
        }

        public IActionResult CreateEditJobPosting(int Id)
        {
            return View();
        }

        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile companyImage)
        {
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
                _context.JobPostings.Add(jobPosting);
            }

            _context.SaveChanges(); //Datenbank speichern

            //TODO: write jobposting to db
            return RedirectToAction("Index");
        }
    }
}

