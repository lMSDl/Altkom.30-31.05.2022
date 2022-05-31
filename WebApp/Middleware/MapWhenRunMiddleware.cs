using WebApp.Services;

namespace WebApp.Middleware
{
    public class MapWhenRunMiddleware : RunMiddleware
    {
        public MapWhenRunMiddleware(RequestDelegate _) : base(_)
        {
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Begin of MapWhenRun");
            await context.Response.WriteAsync($"{context.Request.Query["name"]} under construction");
            Console.WriteLine("End of MapWhenRun");
        }
    }
}
