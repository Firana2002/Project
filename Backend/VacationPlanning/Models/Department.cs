public class Department
{
    public int Id { get; set; }
    public string NameDepartments { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}