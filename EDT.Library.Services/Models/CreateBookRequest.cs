namespace EDT.Library.Services.Models;

public record CreateBookRequest
{
	public required string Title { get; init; }
	public required int PublishedYear { get; init; }
	public required string Author { get; init; }
	public required string Description { get; init; }
}