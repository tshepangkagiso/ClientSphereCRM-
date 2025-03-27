namespace CRM_EMPLOYEE_APP.Models;

public class ClientType
{
    public int TypeID { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
