namespace Boss.Gateway.Api.Middleware
{
    public static class MiddlewareExtensionsw
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
