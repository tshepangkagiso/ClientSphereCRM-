using System;
using System.Collections.Generic;

namespace CRM_API.Models;

public partial class EmployeeLogin
{
    public int LoginID { get; set; }

    public string LoginUsername { get; set; } = null!;

    public string LoginPassword { get; set; } = null!;

    public Guid EmployeeID { get; set; }
}
