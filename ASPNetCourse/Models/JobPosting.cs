namespace ASPNetCourse.Models
{
    public class JobPosting
    {

        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }
        public string JobDescription { get; set; }
        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMail { get; set; }
        public string ContactWebsite { get; set; }
        public byte[] CompanyImage { get; set; }
        public string OwnerUsername { get; set; }

        public JobPosting()
        {

            if (JobTitle == null)
            {
                JobTitle = string.Empty;
            }

            if (JobLocation == null)
            {
                JobLocation = string.Empty;
            }

            if (JobDescription == null)
            {
                JobDescription = string.Empty;
            }
            if (CompanyName == null)
            {
                CompanyName = string.Empty;
            }
            if (ContactPhone == null)
            {
                ContactPhone = string.Empty;
            }
            if (ContactMail == null)
            {
                ContactMail = string.Empty;
            }
            if (ContactWebsite == null)
            {
                ContactWebsite = string.Empty;
            }
        }
    }
}
