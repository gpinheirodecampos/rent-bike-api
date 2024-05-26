using Microsoft.AspNetCore.Mvc.Filters;

namespace Rents.Api.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        // Injecao de dependencia
        private readonly ILogger _logger;
        public ApiLoggingFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger(""); ;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // montando log
            var logRequest = context.HttpContext.Request.Method + " " + context.HttpContext.Request.Scheme + "://" + context.HttpContext.Request.Host + context.HttpContext.Request.Path;

            var logResponse = context.HttpContext.Response.StatusCode == 200 ? "200 OK!" : $"{context.HttpContext.Response.StatusCode} FAIL!";

            _logger.LogInformation("##################################");
            _logger.LogInformation("### Executando -> OnActionExecuting");
            _logger.LogInformation($"### Horário: {DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"### Requisição: {logRequest}");
            _logger.LogInformation($"### Resposta: {logResponse}");
            _logger.LogInformation("##################################");
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            var logRequest = context.HttpContext.Request.Method + " " + context.HttpContext.Request.Scheme + "://" + context.HttpContext.Request.Host + context.HttpContext.Request.Path;

            var logResponse = context.HttpContext.Response.StatusCode == 200 ? "200 OK!" : $"{context.HttpContext.Response.StatusCode} FAIL!";

            _logger.LogInformation("##################################");
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation($"### Horário: {DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"### Requisição: {logRequest}");
            _logger.LogInformation($"### Resposta: {logResponse}");
            _logger.LogInformation("##################################");
        }

    }
}
