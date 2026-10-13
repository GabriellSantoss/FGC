using System.Net;
using System.Text.Json;

namespace JogoService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;

                var resposta = new
                {
                    status = context.Response.StatusCode,
                    mensagem = ex.Message
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(resposta));
            }
        }
    }
}