namespace CodeFirst.Middlewares
{
    public class APIKey
    {

        private readonly RequestDelegate _next;
        private const string nameapikey = "ApiKey";

        public APIKey(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            if (!context.Request.Headers.TryGetValue(nameapikey, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No hay api key.");
                return;
            }

            var apiKey = configuration["ApiKey"];

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No tienes autorizacion.");
                return;
            }

            await _next(context);
        }
    }
}
