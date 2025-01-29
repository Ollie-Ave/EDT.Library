namespace EDT.Library.Services.Models;

public record BookResult
{
	public required int Id { get; init; }
	public required string Title { get; init; }
	public required int PublishedYear { get; init; }
	public required string Author { get; init; }
	public required string Description { get; init; }
}