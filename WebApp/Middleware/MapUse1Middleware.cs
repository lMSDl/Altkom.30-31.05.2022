using WebApp.Services;

namespace WebApp.Middleware
{
    public class MapUse1Middleware : IMiddleware
    {
        public MapUse1Middleware(HelloService helloService)
        {
            helloService.Run();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Begin of MapUse1");
            await next(context);
            Console.WriteLine("End of MapUse1");
        }
    }
}
