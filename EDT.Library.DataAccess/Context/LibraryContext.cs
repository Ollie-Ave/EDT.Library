namespace EDT.LIbrary.DataAccess.Context;

using EDT.Library.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
	public virtual DbSet<Book> Books { get; set; }

	public string DbPath { get; }

	public LibraryContext()
	{
		// In a real-world scenario, this would be in a config file.
		DbPath = "/home/oliver/Code/EDT.Library/Library.db";
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		BuildBooks(modelBuilder);
	}

	private void BuildBooks(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Book>()
			.HasKey(b => b.Id);

		modelBuilder.Entity<Book>()
			.Property(b => b.Title)
			.HasMaxLength(32)
			.IsRequired();

		modelBuilder.Entity<Book>()
			.Property(b => b.Author)
			.HasMaxLength(32)
			.IsRequired();

		modelBuilder.Entity<Book>()
			.Property(b => b.Description)
			.HasMaxLength(256)
			.IsRequired();

		modelBuilder.Entity<Book>()
			.Property(b => b.PublishedYear)
			.IsRequired();
	}
}
