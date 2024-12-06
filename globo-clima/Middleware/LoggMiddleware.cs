using System.Diagnostics;

namespace api_animes.Middleware
{
    public class LoggMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var conometro = Stopwatch.StartNew();
            var verbo = context.Request.Method;
            var caminhoDaRequisicao = context.Request.Path;

            await _next(context);

            conometro.Stop();
            
            var respostaStatusCode = context.Response.StatusCode;
            var pingMs = conometro.ElapsedMilliseconds;

            Console.WriteLine($"Requisição: {verbo} {caminhoDaRequisicao} respondeu com o status {respostaStatusCode} em {pingMs}ms");
        }
    }
}
