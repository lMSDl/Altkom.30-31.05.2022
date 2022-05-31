using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWebService<WebUser>, WebService<WebUser>>();
builder.Services.AddTransient<EntityFaker<WebUser>, WebUserFaker>();

var app = builder.Build();

//app.Map("/WebUsers", webUsersApp =>
//{
//    webUsersApp.UseRouting();
//    webUsersApp.UseEndpoints(enpoints =>
//    {
//        enpoints.MapGet("/", async context => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync()));
//        enpoints.MapGet("/{id:int}", async context =>
//        {
//            if (context.Request.RouteValues.TryGetValue("id", out var id))
//            {
//                var entity = await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync(int.Parse(id.ToString()));
//                if (entity != null)
//                    await context.Response.WriteAsJsonAsync(entity);
//            }
//        });
//        enpoints.MapDelete("/{id:int}", async context =>
//        {
//            if (context.Request.RouteValues.TryGetValue("id", out var id))
//            {
//                await context.RequestServices.GetService<IWebService<WebUser>>()!.DeleteAsync(int.Parse(id.ToString()));
//                context.Response.StatusCode = StatusCodes.Status204NoContent;
//            }
//        });
//    });

//});

app.MapGet("/WebUsers", async context => await context.Response.WriteAsJsonAsync(await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync()));
app.MapGet("/WebUsers/{id:int}", async context =>
{
    if (context.Request.RouteValues.TryGetValue("id", out var id))
    {
        var entity = await context.RequestServices.GetService<IWebService<WebUser>>()!.ReadAsync(int.Parse(id.ToString()));
        if (entity != null)
            await context.Response.WriteAsJsonAsync(entity);
    }
});
app.MapDelete("/WebUsers/{id:int}", async context =>
{
    if (context.Request.RouteValues.TryGetValue("id", out var id))
    {
        await context.RequestServices.GetService<IWebService<WebUser>>()!.DeleteAsync(int.Parse(id.ToString()));
        context.Response.StatusCode = StatusCodes.Status204NoContent;
    }
});


app.MapGet("/", () => "Hello World!");

app.Run();
