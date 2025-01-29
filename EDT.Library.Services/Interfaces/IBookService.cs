using EDT.Library.Services.Models;

namespace EDT.Library.Services.Interfaces;

public interface IBookService
{
	Task<List<BookResult>> GetBooksAsync(CancellationToken cancellationToken);

	Task DeleteBookAsync(int id);

	Task UpdateBookAsync(UpdateBookRequest request);

	Task CreateBookAsync(CreateBookRequest request);
}