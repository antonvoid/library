using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Series
{
    public int SeriesId { get; set; }

    public string? Title { get; set; }

    public int? NumberOfBooks { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
