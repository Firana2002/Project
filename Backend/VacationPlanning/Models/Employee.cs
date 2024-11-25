public class Employee
{
    public int Id { get; set; }
    public string NameEmployees { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}