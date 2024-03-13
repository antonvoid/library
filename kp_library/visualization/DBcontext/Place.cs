using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Place
{
    public int PlaceId { get; set; }

    public int? Room { get; set; }

    public int? Line { get; set; }

    public int? Shelf { get; set; }

    public int? Position { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
