public class EmployeeVacationBalance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int VacationTypeId { get; set; }
    public int Days { get; set; }
    public Employee Employee { get; set; }
    public VacationType VacationType { get; set; }
}