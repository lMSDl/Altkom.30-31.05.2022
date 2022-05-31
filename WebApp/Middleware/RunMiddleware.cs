using WebApp.Services;

namespace WebApp.Middleware
{
    public abstract class RunMiddleware
    {

        public RunMiddleware(RequestDelegate _)
        {
        }

        public abstract Task InvokeAsync(HttpContext context);
    }
}
