using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SkillSnap.Api.Models;
using System.Diagnostics;

namespace SkillSnap.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectsController> _logger;
    private readonly IMemoryCache _cache;

    public ProjectsController(ApplicationDbContext context, ILogger<ProjectsController> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    // GET: api/Projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        var stopwatch = Stopwatch.StartNew();

        string cacheKey = "projects_all";
        if (_cache.TryGetValue(cacheKey, out List<Project>? cachedProjects))
        {
            stopwatch.Stop();
            _logger.LogInformation("Cache hit for projects. Duration: {Duration} ms", stopwatch.ElapsedMilliseconds);
            return cachedProjects!;
        }

        var projects = await _context.Projects
            .AsNoTracking()
            .ToListAsync();

        _cache.Set(cacheKey, projects, TimeSpan.FromMinutes(5));

        stopwatch.Stop();
        _logger.LogInformation("Cache miss for projects. Duration: {Duration} ms", stopwatch.ElapsedMilliseconds);

        return projects;
    }

    // GET: api/Projects/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
        // AsNoTracking for read-only, Include PortfolioUser
        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }
        return project;
    }

    // POST: api/Projects
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    // PUT: api/Projects/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutProject(int id, Project project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProjectExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Projects/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}
