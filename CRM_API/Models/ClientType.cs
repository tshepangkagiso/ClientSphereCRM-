using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM_API.Models;

public partial class ClientType
{
    [Key]
    [Column("TypeID")]
    public int TypeID { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
