﻿
using Microsoft.Extensions.Configuration;

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
            .Build();


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