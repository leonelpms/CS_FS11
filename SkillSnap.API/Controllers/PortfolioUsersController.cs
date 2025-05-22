using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSnap.Api.Models;

namespace SkillSnap.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PortfolioUsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PortfolioUsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/PortfolioUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PortfolioUser>>> GetPortfolioUsers()
    {
        return await _context.PortfolioUsers
            .Include(u => u.Projects)
            .Include(u => u.Skills)
            .ToListAsync();
    }

    // GET: api/PortfolioUsers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PortfolioUser>> GetPortfolioUser(int id)
    {
        var user = await _context.PortfolioUsers
            .Include(u => u.Projects)
            .Include(u => u.Skills)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST: api/PortfolioUsers
    [HttpPost]
    public async Task<ActionResult<PortfolioUser>> PostPortfolioUser(PortfolioUser user)
    {
        _context.PortfolioUsers.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPortfolioUser), new { id = user.Id }, user);
    }

    // PUT: api/PortfolioUsers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPortfolioUser(int id, PortfolioUser user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PortfolioUserExists(id))
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

    // DELETE: api/PortfolioUsers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePortfolioUser(int id)
    {
        var user = await _context.PortfolioUsers.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.PortfolioUsers.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PortfolioUserExists(int id)
    {
        return _context.PortfolioUsers.Any(e => e.Id == id);
    }
}
