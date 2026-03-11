using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JobPortalAPI.Data;
using JobPortalAPI.Models;

namespace JobPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("apply")]
        public IActionResult ApplyJob(Application application)
        {
            application.ApplicationDate = DateTime.Now;
            application.Status = "Applied";

            _context.Applications.Add(application);
            _context.SaveChanges();

            return Ok(application);
        }

        [Authorize]
        [HttpGet("my/{userId}")]
        public IActionResult GetMyApplications(int userId)
        {
            var apps = _context.Applications.Where(a => a.UserId == userId).ToList();
            return Ok(apps);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllApplications()
        {
            return Ok(_context.Applications.ToList());
        }
    }
}