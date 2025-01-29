namespace EDT.Library.Api.Controllers;

using EDT.Library.Services.Interfaces;
using EDT.Library.Services.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
	private readonly ILogger<BooksController> logger;
	private readonly IBookService bookService;

	public BooksController(ILogger<BooksController> logger, IBookService bookService)
	{
		this.logger = logger;
		this.bookService = bookService;
	}

	[HttpGet]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		try
		{
			var books = await bookService.GetBooksAsync(cancellationToken);

			return Ok(books);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while getting books.");

			return Problem();
		}
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateBookRequest request)
	{
		try
		{
			await bookService.UpdateBookAsync(request);

			return Ok();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while creating a book.");

			return Problem();
		}
	}

	[HttpPost("[action]/{id}")]
	public async Task<IActionResult> DeleteAsync([FromRoute] int id)
	{
		try
		{
			await bookService.DeleteBookAsync(id);

			return Ok();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while creating a book.");

			return Problem();
		}
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> CreateAsync([FromBody] CreateBookRequest request)
	{
		try
		{
			await bookService.CreateBookAsync(request);

			return Ok();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while creating a book.");

			return Problem();
		}
	}
}