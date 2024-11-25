
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<VacationType> VacationTypes { get; set; }
    public DbSet<EmployeeVacationBalance> EmployeeVacationBalances { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<Restriction> Restrictions { get; set; }
    public DbSet<DepartmentRestriction> DepartmentRestrictions { get; set; }
    public DbSet<ScheduledVacation> ScheduledVacations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>().HasData(new Organization { Id = 1, NameOrganization = "Эн+ Диджитал" });
        modelBuilder.Entity<VacationType>().HasData(
            new VacationType { Id = 1, NameVacationTypes = "ежегодный основной" },
            new VacationType { Id = 2, NameVacationTypes = "ежегодный дополнительный" },
            new VacationType { Id = 3, NameVacationTypes = "учебный" },
            new VacationType { Id = 4, NameVacationTypes = "без сохранения зарплаты" }
        );
        modelBuilder.Entity<Restriction>().HasData(
            new Restriction { Id = 1, Type = "законодательное" },
            new Restriction { Id = 2, Type = "корпоративное" }
        );
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
