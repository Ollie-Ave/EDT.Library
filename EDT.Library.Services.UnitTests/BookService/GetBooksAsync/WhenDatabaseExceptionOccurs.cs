using EDT.LIbrary.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EDT.Library.Services.UnitTests.BookService.GetBooksAsync;

[TestFixture]
public class WhenDatabaseExceptionOccurs
{
	[Test]
	public void ThenDatabaseExceptionIsThrown()
	{
		// Arrange
		var mockContext = CreateMockContextThatThrows();
		var mockContextFactory = CreateMockDbContextFactoryThatReturnsMockContext(mockContext);
		Services.BookService subjectUnderTest = new(mockContextFactory.Object);

		// Act &  Assert
		Assert.ThrowsAsync<Exception>(() => subjectUnderTest.GetBooksAsync(CancellationToken.None));
	}

	private Mock<LibraryContext> CreateMockContextThatThrows()
	{
		Mock<LibraryContext> mockContext = new();

		mockContext
			.Setup(x => x.Books)
			.Throws<Exception>();

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
