using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_API.Models;

public partial class ClientsLogin
{
    [Key]
    [Column("LoginID")]
    public int LoginID { get; set; }

    public string LoginUsername { get; set; } = null!;

    public string LoginPassword { get; set; } = null!;

    public Guid ClientID { get; set; }

    public virtual Client? Client { get; set; }
}
