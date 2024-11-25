// Controllers/RestrictionsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class RestrictionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RestrictionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restriction>>> GetRestrictions()
    {
        return await _context.Restrictions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Restriction>> GetRestriction(int id)
    {
        var restriction = await _context.Restrictions.FindAsync(id);
        if (restriction == null)
        {
            return NotFound();
        }
        return restriction;
    }

    [HttpPost]
    public async Task<ActionResult<Restriction>> PostRestriction(Restriction restriction)
    {
        _context.Restrictions.Add(restriction);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRestriction), new { id = restriction.Id }, restriction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRestriction(int id, Restriction restriction)
    {
        if (id != restriction.Id)
        {
            return BadRequest();
        }

        _context.Entry(restriction).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestriction(int id)
    {
        var restriction = await _context.Restrictions.FindAsync(id);
        if (restriction == null)
        {
            return NotFound();
        }

        _context.Restrictions.Remove(restriction);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
