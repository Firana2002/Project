// Controllers/VacationTypesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class VacationTypesController : ControllerBase
{
    private readonly AppDbContext _context;

    public VacationTypesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VacationType>>> GetVacationTypes()
    {
        return await _context.VacationTypes.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VacationType>> GetVacationType(int id)
    {
        var vacationType = await _context.VacationTypes.FindAsync(id);
        if (vacationType == null)
        {
            return NotFound();
        }
        return vacationType;
    }

    [HttpPost]
    public async Task<ActionResult<VacationType>> PostVacationType(VacationType vacationType)
    {
        _context.VacationTypes.Add(vacationType);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVacationType), new { id = vacationType.Id }, vacationType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutVacationType(int id, VacationType vacationType)
    {
        if (id != vacationType.Id)
        {
            return BadRequest();
        }

        _context.Entry(vacationType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacationType(int id)
    {
        var vacationType = await _context.VacationTypes.FindAsync(id);
        if (vacationType == null)
        {
            return NotFound();
        }

        _context.VacationTypes.Remove(vacationType);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
