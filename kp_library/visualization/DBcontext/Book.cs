using System;
using System.Collections.Generic;

namespace Db_lib.Model;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public int? AuthorId { get; set; }

    public int? SeriesId { get; set; }

    public int? CountryId { get; set; }

    public int? PlaceId { get; set; }

    public int? PublisherId { get; set; }

    public int? IssuanceId { get; set; }

    public int? GenreId { get; set; }

    public int? NumberOfPages { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual Place? Place { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual Series? Series { get; set; }

    public virtual ICollection<Issuance> Issuances { get; set; } = new List<Issuance>();
}
