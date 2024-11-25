// Controllers/DepartmentRestrictionsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DepartmentRestrictionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentRestrictionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentRestriction>>> GetDepartmentRestrictions()
    {
        return await _context.DepartmentRestrictions.Include(dr => dr.Department).Include(dr => dr.Restriction).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentRestriction>> GetDepartmentRestriction(int id)
    {
        var restriction = await _context.DepartmentRestrictions.Include(dr => dr.Department).Include(dr => dr.Restriction).FirstOrDefaultAsync(dr => dr.Id == id);
        if (restriction == null)
        {
            return NotFound();
        }
        return restriction;
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentRestriction>> PostDepartmentRestriction(DepartmentRestriction restriction)
    {
        _context.DepartmentRestrictions.Add(restriction);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartmentRestriction), new { id = restriction.Id }, restriction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartmentRestriction(int id, DepartmentRestriction restriction)
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
    public async Task<IActionResult> DeleteDepartmentRestriction(int id)
    {
        var restriction = await _context.DepartmentRestrictions.FindAsync(id);
        if (restriction == null)
        {
            return NotFound();
        }

        _context.DepartmentRestrictions.Remove(restriction);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
