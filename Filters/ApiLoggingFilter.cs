using Microsoft.AspNetCore.Mvc.Filters;

namespace RentAPI.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        // Injecao de dependencia
        private readonly ILogger<ApiLoggingFilter> _logger;
        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("## Executando -> OnActionExecuting");
            _logger.LogInformation("##################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"Model State: {context.ModelState.IsValid}");
            _logger.LogInformation("##################################");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("## Executando -> OnActionExecuted");
            _logger.LogInformation("##################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation("##################################");
        }

    }
}
