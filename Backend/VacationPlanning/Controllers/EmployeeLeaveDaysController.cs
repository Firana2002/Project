// Controllers/EmployeeVacationBalancesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EmployeeVacationBalancesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeeVacationBalancesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeVacationBalance>>> GetEmployeeVacationBalances()
    {
        return await _context.EmployeeVacationBalances.Include(ev => ev.Employee).Include(ev => ev.VacationType).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeVacationBalance>> GetEmployeeVacationBalance(int id)
    {
        var balance = await _context.EmployeeVacationBalances.Include(ev => ev.Employee).Include(ev => ev.VacationType).FirstOrDefaultAsync(ev => ev.Id == id);
        if (balance == null)
        {
            return NotFound();
        }
        return balance;
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeVacationBalance>> PostEmployeeVacationBalance(EmployeeVacationBalance balance)
    {
        _context.EmployeeVacationBalances.Add(balance);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEmployeeVacationBalance), new { id = balance.Id }, balance);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployeeVacationBalance(int id, EmployeeVacationBalance balance)
    {
        if (id != balance.Id)
        {
            return BadRequest();
        }

        _context.Entry(balance).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeVacationBalance(int id)
    {
        var balance = await _context.EmployeeVacationBalances.FindAsync(id);
        if (balance == null)
        {
            return NotFound();
        }

        _context.EmployeeVacationBalances.Remove(balance);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
