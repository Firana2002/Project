// Controllers/DepartmentsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await _context.Departments.Include(d => d.Organization).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _context.Departments.Include(d => d.Organization).FirstOrDefaultAsync(d => d.Id == id);
        if (department == null)
        {
            return NotFound();
        }
        return department;
    }

    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, Department department)
    {
        if (id != department.Id)
        {
            return BadRequest();
        }

        _context.Entry(department).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
