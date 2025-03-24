using System;
using System.Collections.Generic;

namespace CRM_API.Models;

public partial class Title
{
    public int TitleId { get; set; }

    public string Title1 { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
