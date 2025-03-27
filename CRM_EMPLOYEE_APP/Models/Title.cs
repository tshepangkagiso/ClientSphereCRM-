namespace CRM_EMPLOYEE_APP.Models;

public class Title
{
    public int TitleID { get; set; }

    public string TitleName { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
