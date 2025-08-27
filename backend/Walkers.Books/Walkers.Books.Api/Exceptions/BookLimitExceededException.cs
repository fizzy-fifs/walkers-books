namespace Walkers.Books.Api.Exceptions;

public class BookLimitExceededException(string message) : Exception(message);