using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


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
