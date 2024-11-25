// Controllers/OrganizationsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrganizationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
    {
        return await _context.Organizations.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Organization>> GetOrganization(int id)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization == null)
        {
            return NotFound();
        }
        return organization;
    }

    [HttpPost]
    public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
    {
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrganization), new { id = organization.Id }, organization);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrganization(int id, Organization organization)
    {
        if (id != organization.Id)
        {
            return BadRequest();
        }

        _context.Entry(organization).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(int id)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization == null)
        {
            return NotFound();
        }

        _context.Organizations.Remove(organization);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
