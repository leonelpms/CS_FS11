using Microsoft.AspNetCore.Mvc;
using SkillSnap.Api.Models;
using SkillSnap.API.Data;

namespace SkillSnap.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public SeedController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost]
    public IActionResult Seed()
    {
        if (_context.PortfolioUsers.Any())
        {
            return BadRequest("Sample data already exists.");
        }
        var user = new PortfolioUser
        {
            Nombre = "Jordan Developer",
            Bio = "Full-stack developer passionate about learning new tech.",
            ProfileImageUrl = "https://example.com/images/jordan.png",
            Projects = new List<Project>
{
new Project { Título = "Task Tracker", Descripción = "Manage tasks effectively", ImageUrl = "https://example.com/images/task.png" },
new Project { Título = "Weather App", Descripción = "Forecast weather using APIs", ImageUrl = "https://example.com/images/weather.png" }
},
            Skills = new List<Skill>
{
new Skill { Nombre = "C#", Nivel = "Advanced" },
new Skill { Nombre = "Blazor", Nivel = "Intermediate" }
}
        };
        _context.PortfolioUsers.Add(user);
        _context.SaveChanges();
        return Ok("Sample data inserted.");
    }
}