using GUI.WASM.Server;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using ROGStrixScopeRx.Library;
using ROGStrixScopeRx.Library.Generators;
using ROGStrixScopeRx.Library.Producers;
using ROGStrixScopeRx.Library.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR().AddMessagePackProtocol();
builder.Services.AddSingleton<IDatapool, InternalDataPool>()
    .AddHostedService<DataConsumer>()
    .AddHostedService<Randomizer>()

    //  .AddHostedService<Flasher>()
    .AddHostedService<USBService>().AddLogging()
.AddHostedService<DataPoolWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapHub<DataHub>("/datahub");
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
