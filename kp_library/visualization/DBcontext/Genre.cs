using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreTitle { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
