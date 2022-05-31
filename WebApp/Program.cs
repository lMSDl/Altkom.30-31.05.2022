using WebApp.Middleware;
using WebApp.Services;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HelloService>();
builder.Services.AddScoped<MapUse1Middleware>();

var app = builder.Build();

Console.WriteLine(app.Configuration.GetConnectionString("Database"));



app.Use(async (context, next) =>
{
    Console.WriteLine("Begin of Use1");
    await next();
    Console.WriteLine("End of Use1");
});


app.Map("/map", x => CofigureMap(x, app.Environment));


app.Use2();

//app.UseMiddleware<Use2Middleware>();

//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Begin of Use2");
//    await next();
//    Console.WriteLine("End of Use2");
//});

app.MapWhen(context => context.Request.Query.TryGetValue("name", out _), mapWhenApp =>
{
    mapWhenApp.MapWhenRun();
    
    //mapWhenApp.UseMiddleware<MapWhenRunMiddleware>();
    
    //mapWhenApp.Run(async context =>
    //{
    //    Console.WriteLine("Begin of MapWhenRun");
    //    await context.Response.WriteAsync($"{context.Request.Query["name"]} under construction");
    //    Console.WriteLine("End of MapWhenRun");
    //});
});

app.Run(async context =>
{
    Console.WriteLine("Begin of Run");
    await context.Response.WriteAsync("Under construction");
    Console.WriteLine("End of Run");
});



switch (app.Environment.EnvironmentName)
{
    case nameof(EnvironmentName.Development):
        app.MapGet("/", () => "Hello from Development!");
        break;
    case nameof(EnvironmentName.Production):
        app.MapGet("/", () => "Hello from Production!");
        break;
    case nameof(EnvironmentName.Staging):
        app.MapGet("/", () => "Hello from Staging!");
        break;
    default:
        app.MapGet("/", () => $"Hello from {app.Environment.EnvironmentName}!");
        break;
}


app.Run();



void CofigureMap(IApplicationBuilder mapApp, IWebHostEnvironment env)
{
    if(env.IsDevelopment())
        mapApp.MapUse1();

    //mapApp.UseMiddleware<MapUse1Middleware>();

    //mapApp.Use(async (context, next) =>
    //{
    //    Console.WriteLine("Begin of MapUse1");
    //    await next();
    //    Console.WriteLine("End of MapUse1");
    //});

    mapApp.Run(async context =>
    {
        Console.WriteLine("Begin of MapRun");
        await context.Response.WriteAsync("Map under construction");
        Console.WriteLine("End of MapRun");
    });

}