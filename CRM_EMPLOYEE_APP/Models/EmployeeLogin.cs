

namespace CRM_EMPLOYEE_APP.Models;

public class EmployeeLogin
{
    public int LoginID { get; set; }

    public string LoginUsername { get; set; } = null!;

    public string LoginPassword { get; set; } = null!;

    public Guid EmployeeID { get; set; }
}
