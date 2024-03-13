using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Db_lib.Model;

public partial class KpContext : DbContext
{
    public KpContext()
    {
    }

    public KpContext(DbContextOptions<KpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Issuance> Issuances { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=kp;Username=user1;Password=1234;SSL Mode=Prefer;Timeout=10;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("author_pkey");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfDeath)
                .HasMaxLength(20)
                .HasColumnName("date_of_death");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(200)
                .HasColumnName("middle_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("book_pkey");

            entity.ToTable("book");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.IssuanceId).HasColumnName("issuance_id");
            entity.Property(e => e.NumberOfPages).HasColumnName("number_of_pages");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.SeriesId).HasColumnName("series_id");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("book_author_id_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Books)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("book_country_id_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("book_genre_id_fkey");

            entity.HasOne(d => d.Place).WithMany(p => p.Books)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("book_place_id_fkey");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("book_publisher_id_fkey");

            entity.HasOne(d => d.Series).WithMany(p => p.Books)
                .HasForeignKey(d => d.SeriesId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("book_series_id_fkey");

            entity.HasMany(d => d.Issuances).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksIssuance",
                    r => r.HasOne<Issuance>().WithMany()
                        .HasForeignKey("IssuanceId")
                        .HasConstraintName("books_issuance_issuance_id_fkey"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("books_issuance_book_id_fkey"),
                    j =>
                    {
                        j.HasKey("BookId", "IssuanceId").HasName("books_issuance_pkey");
                        j.ToTable("books_issuance");
                        j.IndexerProperty<int>("BookId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("book_id");
                        j.IndexerProperty<int>("IssuanceId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("issuance_id");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeePhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("employee_phone_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(200)
                .HasColumnName("middle_name");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genre_pkey");

            entity.ToTable("genre");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.GenreTitle)
                .HasMaxLength(200)
                .HasColumnName("genre_title");
        });

        modelBuilder.Entity<Issuance>(entity =>
        {
            entity.HasKey(e => e.IssuanceId).HasName("issuance_pkey");

            entity.ToTable("issuance");

            entity.Property(e => e.IssuanceId).HasColumnName("issuance_id");
            entity.Property(e => e.DateOfIssuance).HasColumnName("date_of_issuance");
            entity.Property(e => e.DateOfReturn).HasColumnName("date_of_return");
            entity.Property(e => e.VisitorId).HasColumnName("visitor_id");

            entity.HasOne(d => d.Visitor).WithMany(p => p.Issuances)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("issuance_visitor_id_fkey");

            entity.HasMany(d => d.Employees).WithMany(p => p.Issuances)
                .UsingEntity<Dictionary<string, object>>(
                    "IssuanceEmployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("issuance_employee_employee_id_fkey"),
                    l => l.HasOne<Issuance>().WithMany()
                        .HasForeignKey("IssuanceId")
                        .HasConstraintName("issuance_employee_issuance_id_fkey"),
                    j =>
                    {
                        j.HasKey("IssuanceId", "EmployeeId").HasName("issuance_employee_pkey");
                        j.ToTable("issuance_employee");
                        j.IndexerProperty<int>("IssuanceId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("issuance_id");
                        j.IndexerProperty<int>("EmployeeId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("employee_id");
                    });
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("place_pkey");

            entity.ToTable("place");

            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.Line).HasColumnName("line");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.Room).HasColumnName("room");
            entity.Property(e => e.Shelf).HasColumnName("shelf");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("publisher_pkey");

            entity.ToTable("publisher");

            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.PublisherTitle)
                .HasMaxLength(200)
                .HasColumnName("publisher_title");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.SeriesId).HasName("series_pkey");

            entity.ToTable("series");

            entity.Property(e => e.SeriesId).HasColumnName("series_id");
            entity.Property(e => e.NumberOfBooks).HasColumnName("number_of_books");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("visitor_pkey");

            entity.ToTable("visitor");

            entity.Property(e => e.VisitorId).HasColumnName("visitor_id");
            entity.Property(e => e.Addres)
                .HasMaxLength(200)
                .HasColumnName("addres");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("first_name");
            entity.Property(e => e.IssuanceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("issuance_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(200)
                .HasColumnName("middle_name");
            entity.Property(e => e.VisitorPhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("visitor_phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
