namespace JobPortalAPI.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public string SalaryRange { get; set; }
        public DateTime PostedDate { get; set; }
    }
}