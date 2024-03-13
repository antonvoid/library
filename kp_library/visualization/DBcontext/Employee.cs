using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? EmployeePhoneNumber { get; set; }

    public string? Position { get; set; }

    public virtual ICollection<Issuance> Issuances { get; set; } = new List<Issuance>();
}
