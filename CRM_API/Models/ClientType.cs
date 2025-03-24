using System;
using System.Collections.Generic;

namespace CRM_API.Models;

public partial class ClientType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
