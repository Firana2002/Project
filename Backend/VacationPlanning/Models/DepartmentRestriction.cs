public class DepartmentRestriction
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int RestrictionId { get; set; }
    public Department Department { get; set; }
    public Restriction Restriction { get; set; }
}