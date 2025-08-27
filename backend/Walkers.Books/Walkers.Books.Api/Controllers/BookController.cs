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