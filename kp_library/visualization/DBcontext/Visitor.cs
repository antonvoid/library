using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public int IssuanceId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? Addres { get; set; }

    public string? VisitorPhoneNumber { get; set; }

    public virtual ICollection<Issuance> Issuances { get; set; } = new List<Issuance>();
}
