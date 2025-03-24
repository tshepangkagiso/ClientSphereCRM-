using System;
using System.Collections.Generic;

namespace CRM_API.Models;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public int? TitleId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeSurname { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Title? Title { get; set; }
}
