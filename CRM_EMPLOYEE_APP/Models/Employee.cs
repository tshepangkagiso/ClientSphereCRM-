
namespace CRM_EMPLOYEE_APP.Models;

public class Employee
{
    public Guid EmployeeID { get; set; }

    public int? TitleID { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeSurname { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Title? Title { get; set; }
}
