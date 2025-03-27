namespace CRM_EMPLOYEE_APP.Models;

public class ClientsLogin
{
    public int LoginID { get; set; }

    public string LoginUsername { get; set; } = null!;

    public string LoginPassword { get; set; } = null!;

    public Guid ClientID { get; set; }

    public virtual Client? Client { get; set; }
}
