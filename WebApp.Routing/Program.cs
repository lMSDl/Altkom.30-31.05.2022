var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");
    await next();

});

app.UseRouting();


app.Use(async (context, next) =>
{
    Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "NULL");
    await next();

});

app.Map("/Bye", mapApp =>
{
    mapApp.UseRouting();
    mapApp.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/Bye", () => "Bye!");

    });

    mapApp.Run(async context =>
    {
        await context.Response.WriteAsync("Hello!");
    });
});



app.MapGet("/", () => "Hello World!");
app.MapGet("/Bye", () => "Bye!");
app.MapGet("/Bye/{name:min(10)}", (string name) => $"Bye {name}!");



app.Run();
