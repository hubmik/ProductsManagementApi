using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var isBadRequest = exception is ArgumentException or ArgumentOutOfRangeException;
            var statusCode = isBadRequest
                ? StatusCodes.Status400BadRequest
                : StatusCodes.Status500InternalServerError;

            logger.LogError(
                exception,
                "Błąd podczas {Method} {Path} (status {StatusCode})",
                context.Request.Method,
                context.Request.Path,
                statusCode);

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = statusCode,
                Title = isBadRequest
                    ? "Nieprawidłowe dane żądania."
                    : "Wystąpił nieoczekiwany błąd serwera.",
                Detail = exception.Message
            }, cancellationToken);

            return true;
        }
    }
}
