using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Walkers.Books.Api.Exceptions;
using Walkers.Books.Api.Models;
using Walkers.Books.Api.Models.Requests;
using Walkers.Books.Api.Services;
using Walkers.Books.Api.Validators;

namespace Walkers.Books.Api.Controllers;

[Route("api/v1.0/books")]
[ApiController]
public class BookController(IBookService bookService)
{
    [HttpPost("create")]
    public async Task<ActionResult<Book>> CreateBookAsync(CreateBookRequest createBookRequest)
    {
        var validationResult = await ValidateRequest(createBookRequest);

        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult);
        }

        try
        {
            return await bookService.CreateBookAsync(createBookRequest);
        }
        catch (BookLimitExceededException ex)
        {
            return new ConflictObjectResult(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return new ConflictObjectResult(ex.Message);
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<Book>> UpdateBookAsync(UpdateBookRequest updateBookRequest)
    {
        var validationResult = await ValidateRequest(updateBookRequest);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult);
        }
        try
        {
            return await bookService.UpdateBookAsync(updateBookRequest);
        }
        catch (InvalidOperationException ex)
        {
            return new NotFoundObjectResult(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBookAsync(Guid id)
    {
        try
        {
            await bookService.DeleteBookAsync(id);
            return new NoContentResult();
        }
        catch (InvalidOperationException ex)
        {
            return new NotFoundObjectResult(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookByIdAsync(Guid id)
    {
        var book = await bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return new NotFoundResult();
        }
        return new OkObjectResult(book);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooksAsync([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var books = await bookService.GetBooksAsync(search, sortBy, page, pageSize);
        return new OkObjectResult(books);
    }

    private static async Task<ValidationResult> ValidateRequest<T>(T request)
    {
        object validator = request switch
        {
            CreateBookRequest => new CreateBookRequestValidator(),
            UpdateBookRequest => new UpdateBookRequestValidator(),
            _ => throw new ArgumentOutOfRangeException(nameof(request),
                $"No validator found for type {request?.GetType().Name}")
        };
        return await ((IValidator<T>)validator).ValidateAsync(request);
    }
}