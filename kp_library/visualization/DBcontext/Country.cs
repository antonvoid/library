using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
