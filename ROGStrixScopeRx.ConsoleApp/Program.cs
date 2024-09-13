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
using ROGStrixScopeRx.Library.Generators;


// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello, World!");

//InternalDataPool _pool = new InternalDataPool();



HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
IHostEnvironment env = builder.Environment;
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

builder.Services
    // .AddSingleton<IConnection, MqttService>()

    //.AddSingleton(_pool).
    //.AddTransient<IWinService, PerfmonService>()
    //
      .AddSingleton<IDatapool, InternalDataPool>()
      .AddHostedService<DataPoolWorker>()
      //.AddHostedService<Flasher>()
      .AddHostedService<VolumeService>()
      //.AddHostedService<PerfmonService>()
      .AddHostedService<USBService>().AddLogging()
      //
     // .AddHostedService<PerfmonService>()
     // .AddHostedService<VolumeService>()

    // .AddHostedService<FadeService>().AddLogging()
    .Configure<AppSettings>(options => builder.Configuration.GetSection("Config").Bind(options));

// Check if the environment is Windows
if (OperatingSystem.IsWindows())
{
    builder.Services.AddHostedService<PerfmonService>();
}
builder.Logging.AddConsole();
using IHost host = builder.Build(); 
await host.RunAsync();
