
using ConsoleApp;
using ConsoleApp.Configuration.Models;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

//Microsoft.Extensions.Configuration
var config = new ConfigurationBuilder()
            //package Microsoft.Extensions.Configuration.FileExtensions
            //package Microsoft.Extensions.Configuration.Xml
            .AddXmlFile("Configuration/config.xml", optional: true)
            //package Microsoft.Extensions.Configuration.Json
            .AddJsonFile("Configuration/config.json", optional: false, reloadOnChange: true)
            //package Microsoft.Extensions.Configuration.ini
            .AddIniFile("Configuration/config.ini", optional: true)
            //package NetEscapades.Configuration.Yaml
            .AddYamlFile("Configuration/config.yaml", optional: true)
            //package Microsoft.Extensions.Configuration.EnvironmentVariables
            .AddEnvironmentVariables()
            .Build();


IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(options =>
    options
    .AddConfiguration(config.GetSection("Logging"))
    //.SetMinimumLevel(LogLevel.Trace)
    .AddConsole()
    .AddDebug()
    .AddEventLog());

//Transient - zawsze nowa instancja
serviceCollection.AddTransient<IFontService, StandardFontService>();
//Sigleton - raz utworzona instancja 
serviceCollection.AddSingleton<IOutputService, RandomFontConsoleService>();
//Scoped - instancja tworzona dla każdego nowego scope
serviceCollection.AddScoped<IFontService, SubZeroFontService>();


IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

var loggerDemo = new LoggerDemo(serviceProvider.GetService<ILogger<LoggerDemo>>()!);
loggerDemo.Work();

var logger = serviceProvider.GetService<ILogger<Program>>()!;
logger.LogCritical("It works!");


//var fontServices = serviceProvider.GetServices<IFontService>();
//var outputService = serviceProvider.GetService<IOutputService>()!;

using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetService<IOutputService>()!.WriteLine("Hello!");
}
using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetService<IOutputService>()!.WriteLine("Hello!");
}

using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetService<IOutputService>()!.WriteLine("Hello!");
}
using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetService<IOutputService>()!.WriteLine("Hello!");
}
using (var scope = serviceProvider.CreateScope())
{
    scope.ServiceProvider.GetService<IOutputService>()!.WriteLine("Hello!");
}


Console.WriteLine();


static void configuration(IConfigurationRoot config)
{
    //while (true)
    {
        Console.WriteLine($"Hello {config["HelloJson"]}");
        Console.WriteLine($"Hello {config["HelloXml"]}");
        Console.WriteLine($"Hello {config["HelloIni"]}");
        Console.WriteLine($"Hello {config["HelloYaml"]}");
        Console.WriteLine($"{config["Bye"]}");
        Thread.Sleep(1000);
    }


    Console.WriteLine($"{config["Greetings:Greeting1"]} {config["Greetings:Targets:Person"]}");

    var greetingsSerction = config.GetSection("Greetings");
    var targetsSection = greetingsSerction.GetSection("Targets");
    //targetsSection = config.GetSection("Greetings:Targets");

    for (int i = 0; i < int.Parse(config["Repeat"]); i++)
    {
        Console.WriteLine($"{greetingsSerction["Greeting2"]} {targetsSection["IA"]}");
    }

    //package Microsoft.Extensions.Configuration.Binder
    AppConfig appConfig = new();
    config.Bind(appConfig);

    var greetings = new Greetings();
    //config.GetSection(nameof(Greetings)).Bind(greetings);
    greetings = config.GetSection("Greetings").Get<Greetings>();


    //for (int i = 0; i < appConfig.Repeat; i++)
    for (int i = 0; i < config.GetValue<int>("Repeat"); i++)
    {
        Console.WriteLine($"{appConfig.Greetings.Greeting1} {greetings.Targets.Person}");
    }
}