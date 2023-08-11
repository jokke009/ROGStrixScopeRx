using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using ROGStrixScopeRx.Library.Services;
using ROGStrixScopeRx.Library.Controllers;
using ROGStrixScopeRx.Library;
using ROGStrixScopeRx.Library.Model;
using ROGStrixScop.Library.Windows;
using ROGStrixScop.Library.Windows.Producers;


// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello, World!");

InternalDataPool _pool = new InternalDataPool();



HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
IHostEnvironment env = builder.Environment;
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

builder.Services
    // .AddSingleton<IConnection, MqttService>()

    //.AddSingleton(_pool).
    //.AddTransient<IWinService, PerfmonService>()
    //.AddHostedService<PerfmonService>()

    .AddHostedService<USBService>().AddLogging()
    .AddHostedService<VolumeService>()
    // .AddHostedService<FadeService>().AddLogging()
    .Configure<AppSettings>(options => builder.Configuration.GetSection("Config").Bind(options));


builder.Logging.AddConsole();
using IHost host = builder.Build(); 
await host.RunAsync();
