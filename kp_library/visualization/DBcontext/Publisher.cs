using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string? PublisherTitle { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
