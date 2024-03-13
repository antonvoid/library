using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? Country { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? DateOfDeath { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
