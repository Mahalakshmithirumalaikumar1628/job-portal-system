using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetJobs()
        {
            return Ok(_context.Jobs.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetJob(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateJob(Job job)
        {
            job.PostedDate = DateTime.Now;
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return Ok(job);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateJob(int id, Job updatedJob)
        {
            var job = _context.Jobs.Find(id);
            if (job == null) return NotFound();

            job.JobTitle = updatedJob.JobTitle;
            job.CompanyName = updatedJob.CompanyName;
            job.JobDescription = updatedJob.JobDescription;
            job.Location = updatedJob.Location;
            job.SalaryRange = updatedJob.SalaryRange;

            _context.SaveChanges();
            return Ok(job);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            var job = _context.Jobs.Find(id);
            if (job == null) return NotFound();

            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return Ok("Job Deleted");
        }
    }
}