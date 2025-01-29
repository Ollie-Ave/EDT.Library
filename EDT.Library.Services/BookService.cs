using EDT.Library.DataAccess.Models;
using EDT.Library.Services.Interfaces;
using EDT.Library.Services.Models;
using EDT.LIbrary.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EDT.Library.Services;

internal class BookService : IBookService
{
	private readonly IDbContextFactory<LibraryContext> contextFactory;

	public BookService(IDbContextFactory<LibraryContext> contextFactory)
	{
		this.contextFactory = contextFactory;
	}

	public async Task CreateBookAsync(CreateBookRequest request)
	{
		using LibraryContext context = contextFactory.CreateDbContext();

		await context.Books.AddAsync(new Book()
		{
			Title = request.Title,
			Author = request.Author,
			Description = request.Description,
			PublishedYear = request.PublishedYear
		});

		await context.SaveChangesAsync();
	}

	public async Task DeleteBookAsync(int id)
	{
		using LibraryContext context = contextFactory.CreateDbContext();

		var book = await context.Books.SingleAsync(x => x.Id == id);

		context.Books.Remove(book);

		await context.SaveChangesAsync();
	}

	public Task<List<BookResult>> GetBooksAsync(CancellationToken cancellationToken)
	{
		using LibraryContext context = contextFactory.CreateDbContext();

		return context.Books
			.Select(b => new BookResult
			{
				Id = b.Id,
				Title = b.Title,
				PublishedYear = b.PublishedYear,
				Author = b.Author,
				Description = b.Description
			})
			.ToListAsync(cancellationToken);
	}

	public async Task UpdateBookAsync(UpdateBookRequest request)
	{
		using LibraryContext context = contextFactory.CreateDbContext();

		var book = await context.Books.SingleAsync(x => x.Id == request.Id);

		book.Title = request.Title;
		book.Author = request.Author;
		book.Description = request.Description;
		book.PublishedYear = request.PublishedYear;

		await context.SaveChangesAsync();
	}
}