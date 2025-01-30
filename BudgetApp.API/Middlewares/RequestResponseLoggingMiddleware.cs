using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log the request
        var request = await FormatRequest(context.Request);
        _logger.LogInformation($"Incoming Request: {request}");

        // Copy a pointer to the original response body stream
        var originalBodyStream = context.Response.Body;

        // Create a new memory stream to hold the response
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            // Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            // Log the response
            var response = await FormatResponse(context.Response);
            _logger.LogInformation($"Outgoing Response: {response}");

            // Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        var body = request.Body;

        // This line allows us to set the reader for the request back at the beginning of its stream.
        request.EnableBuffering();

        // We now need to read the request stream. First, we create a new byte array with the same length as the request stream.
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        // We then read the entire request stream into the new buffer.
        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        // We convert the byte array to a string using UTF8 encoding.
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        // We need to reset the reader for the request so that the client can read it.
        request.Body.Position = 0;

        // Return the string representation of the request along with the headers.
        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {request.Headers} {bodyAsText}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        // We need to read the response stream from the beginning.
        response.Body.Seek(0, SeekOrigin.Begin);

        // Read the response stream into a string.
        var text = await new StreamReader(response.Body).ReadToEndAsync();

        // We need to reset the reader for the response so that the client can read it.
        response.Body.Seek(0, SeekOrigin.Begin);

        // Return the string representation of the response along with the headers.
        return $"{response.StatusCode}: {response.Headers} {text}";
    }
}