namespace WebApp.Middleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder Use2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Use2Middleware>();
        }

        public static IApplicationBuilder MapUse1(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MapUse1Middleware>();
        }

        public static IApplicationBuilder MapWhenRun(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MapWhenRunMiddleware>();
        }
    }
}
