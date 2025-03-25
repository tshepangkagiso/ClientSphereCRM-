using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM_API.Models;

public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public Guid EmployeeID { get; set; }

    public int? TitleID { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeSurname { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Title? Title { get; set; }
}
