// Controllers/ScheduledVacationsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ScheduledVacationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ScheduledVacationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduledVacation>>> GetScheduledVacations()
    {
        return await _context.ScheduledVacations.Include(sv => sv.Employee).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduledVacation>> GetScheduledVacation(int id)
    {
        var vacation = await _context.ScheduledVacations.Include(sv => sv.Employee).FirstOrDefaultAsync(sv => sv.Id == id);
        if (vacation == null)
        {
            return NotFound();
        }
        return vacation;
    }

    [HttpPost]
    public async Task<ActionResult<ScheduledVacation>> PostScheduledVacation(ScheduledVacation vacation)
    {
        _context.ScheduledVacations.Add(vacation);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetScheduledVacation), new { id = vacation.Id }, vacation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduledVacation(int id, ScheduledVacation vacation)
    {
        if (id != vacation.Id)
        {
            return BadRequest();
        }

        _context.Entry(vacation).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScheduledVacation(int id)
    {
        var vacation = await _context.ScheduledVacations.FindAsync(id);
        if (vacation == null)
        {
            return NotFound();
        }

        _context.ScheduledVacations.Remove(vacation);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
