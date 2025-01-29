using EDT.Library.DataAccess.Models;
using EDT.Library.Services.Models;
using EDT.LIbrary.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace EDT.Library.Services.UnitTests.BookService.GetBooksAsync;

[TestFixture]
public class WhenDatabaseReturnsValidResults
{
	[Test]
	public void ThenValidResultsAreReturned()
	{
		// Arrange
		List<BookResult> expectedResult = [
			new BookResult { Id = 1, Title = "Title 1", Author = "Author 1", Description = "Description 1", PublishedYear = 2021 },
			new BookResult { Id = 2, Title = "Title 2", Author = "Author 2", Description = "Description 2", PublishedYear = 2022 },
			new BookResult { Id = 3, Title = "Title 3", Author = "Author 3", Description = "Description 3", PublishedYear = 2023 }
		];

		var mockContext = CreateMockContextThatReturnsValidResults();
		var mockContextFactory = CreateMockDbContextFactoryThatReturnsMockContext(mockContext);
		Services.BookService subjectUnderTest = new(mockContextFactory.Object);


		// Act
		var actualResult = subjectUnderTest.GetBooksAsync(CancellationToken.None)
			.GetAwaiter()
			.GetResult();

		// Assert
		Assert.That(actualResult, Is.EquivalentTo(expectedResult));
	}

	private Mock<LibraryContext> CreateMockContextThatReturnsValidResults()
	{

		Mock<LibraryContext> mockContext = new();

		mockContext
			.Setup(x => x.Books)
			.ReturnsDbSet(new List<Book>()
		{
			new() { Id = 1, Title = "Title 1", Author = "Author 1", Description = "Description 1", PublishedYear = 2021 },
			new() { Id = 2, Title = "Title 2", Author = "Author 2", Description = "Description 2", PublishedYear = 2022 },
			new() { Id = 3, Title = "Title 3", Author = "Author 3", Description = "Description 3", PublishedYear = 2023 }
		});

		return mockContext;
	}

	private Mock<IDbContextFactory<LibraryContext>> CreateMockDbContextFactoryThatReturnsMockContext(Mock<LibraryContext> mockContext)
	{
		Mock<IDbContextFactory<LibraryContext>> dbContextFactoryMock = new();

		dbContextFactoryMock
			.Setup(x => x.CreateDbContext())
			.Returns(mockContext.Object);

		return dbContextFactoryMock;
	}
}
