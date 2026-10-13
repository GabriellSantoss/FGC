namespace PagamentoService.Middleware
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

                context.Response.StatusCode = ex switch
                {
                    KeyNotFoundException =>
                        StatusCodes.Status404NotFound,

                    InvalidOperationException =>
                        StatusCodes.Status400BadRequest,

                    InvalidDataException =>
                        StatusCodes.Status409Conflict,

                    _ =>
                        StatusCodes.Status500InternalServerError
                };

                var resposta = new
                {
                    status = context.Response.StatusCode,
                    mensagem = ex.Message
                };

                await context.Response.WriteAsJsonAsync(resposta);
            }
        }
    }
}