using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



[Route("api/[controller]")]
[ApiController]
public class ScheduledLeaveController : ControllerBase
{
    private readonly VacationPlanningContext _context;

    public ScheduledLeaveController(VacationPlanningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduledLeave>>> GetScheduledLeaves()
    {
        return await _context.ScheduledLeaves.Include(sl => sl.Employee).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduledLeave>> GetScheduledLeave(int id)
    {
        var scheduledLeave = await _context.ScheduledLeaves.Include(sl => sl.Employee).FirstOrDefaultAsync(sl => sl.Id == id);
        if (scheduledLeave == null) return NotFound();
        return scheduledLeave;
    }

    [HttpPost]
    public async Task<ActionResult<ScheduledLeave>> PostScheduledLeave(ScheduledLeave scheduledLeave)
    {
        _context.ScheduledLeaves.Add(scheduledLeave);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetScheduledLeave", new { id = scheduledLeave.Id }, scheduledLeave);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduledLeave(int id, ScheduledLeave scheduledLeave)
    {
        if (id != scheduledLeave.Id) return BadRequest();
        _context.Entry(scheduledLeave).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ScheduledLeaveExists(id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScheduledLeave(int id)
    {
        var scheduledLeave = await _context.ScheduledLeaves.FindAsync(id);
        if (scheduledLeave == null) return NotFound();

        _context.ScheduledLeaves.Remove(scheduledLeave);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ScheduledLeaveExists(int id)
    {
        return _context.ScheduledLeaves.Any(e => e.Id == id);
    }
}
