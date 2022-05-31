using WebApp.Services;

namespace WebApp.Middleware
{
    public class Use2Middleware
    {
        private readonly RequestDelegate _requestDelegate;

        public Use2Middleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Begin of Use2");
            await _requestDelegate(context);
            Console.WriteLine("End of Use2");
        }
    }
}
