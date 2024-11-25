// Controllers/PositionsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PositionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
    {
        return await _context.Positions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Position>> GetPosition(int id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null)
        {
            return NotFound();
        }
        return position;
    }

    [HttpPost]
    public async Task<ActionResult<Position>> PostPosition(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPosition(int id, Position position)
    {
        if (id != position.Id)
        {
            return BadRequest();
        }

        _context.Entry(position).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePosition(int id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position == null)
        {
            return NotFound();
        }

        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
