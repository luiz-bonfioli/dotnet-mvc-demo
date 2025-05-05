namespace Demo.Infra.Middlewares;

public class HeadersValidationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            // Get the Tenant-ID header value
            var tenantId = context.Request.Headers["X-Tenant-Id"].ToString();

            // Check if the header is missing or invalid
            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("X-Tenant-Id header is required.");
                return;
            }

            // Check if it's a valid GUID
            if (!Guid.TryParse(tenantId, out _))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid X-Tenant-Id format. It should be a valid GUID.");
                return;
            }
        }

        // Proceed to the next middleware or action if valid
        await _next(context);
    }
}
