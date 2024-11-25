// Controllers/HolidaysController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class HolidaysController : ControllerBase
{
    private readonly AppDbContext _context;

    public HolidaysController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Holiday>>> GetHolidays()
    {
        return await _context.Holidays.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Holiday>> GetHoliday(int id)
    {
        var holiday = await _context.Holidays.FindAsync(id);
        if (holiday == null)
        {
            return NotFound();
        }
        return holiday;
    }

    [HttpPost]
    public async Task<ActionResult<Holiday>> PostHoliday(Holiday holiday)
    {
        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetHoliday), new { id = holiday.Id }, holiday);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHoliday(int id, Holiday holiday)
    {
        if (id != holiday.Id)
        {
            return BadRequest();
        }

        _context.Entry(holiday).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHoliday(int id)
    {
        var holiday = await _context.Holidays.FindAsync(id);
        if (holiday == null)
        {
            return NotFound();
        }

        _context.Holidays.Remove(holiday);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
