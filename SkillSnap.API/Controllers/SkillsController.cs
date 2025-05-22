using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SkillSnap.Api.Models;
using System.Diagnostics;

namespace SkillSnap.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectsController> _logger;
    private readonly IMemoryCache _cache;

    public SkillsController(ApplicationDbContext context, ILogger<ProjectsController> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    // GET: api/Skills
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
    {
        var stopwatch = Stopwatch.StartNew();

        string cacheKey = "Skills_all";
        if (_cache.TryGetValue(cacheKey, out List<Skill>? cachedProjects))
        {
            stopwatch.Stop();
            _logger.LogInformation("Cache hit for projects. Duration: {Duration} ms", stopwatch.ElapsedMilliseconds);
            return cachedProjects!;
        }

        var projects = await _context.Skills
            .AsNoTracking()
            .ToListAsync();

        _cache.Set(cacheKey, projects, TimeSpan.FromMinutes(5));

        stopwatch.Stop();
        _logger.LogInformation("Cache miss for projects. Duration: {Duration} ms", stopwatch.ElapsedMilliseconds);

        return projects;
    }

    // GET: api/Skills/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Skill>> GetSkill(int id)
    {
        // AsNoTracking for read-only, Include PortfolioUser
        var project = await _context.Skills
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }
        return project;
    }

    // POST: api/Skills
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Skill>> PostSkill(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
    }

    // PUT: api/Skills/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutSkill(int id, Skill skill)
    {
        if (id != skill.Id)
        {
            return BadRequest();
        }

        _context.Entry(skill).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SkillExists(id))
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

    // DELETE: api/Skills/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null)
        {
            return NotFound();
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SkillExists(int id)
    {
        return _context.Skills.Any(e => e.Id == id);
    }
}
