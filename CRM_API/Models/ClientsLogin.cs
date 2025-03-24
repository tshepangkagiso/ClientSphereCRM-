using System;
using System.Collections.Generic;

namespace CRM_API.Models;

public partial class ClientsLogin
{
    public int LoginId { get; set; }

    public string LoginUsername { get; set; } = null!;

    public string LoginPassword { get; set; } = null!;

    public Guid? ClientId { get; set; }

    public virtual Client? Client { get; set; }
}
