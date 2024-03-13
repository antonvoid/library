using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Issuance
{
    public int IssuanceId { get; set; }

    public int? VisitorId { get; set; }

    public DateOnly? DateOfIssuance { get; set; }

    public DateOnly? DateOfReturn { get; set; }

    public virtual Visitor? Visitor { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
