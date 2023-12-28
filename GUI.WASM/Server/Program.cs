using GUI.WASM.Server;
using Microsoft.AspNetCore.ResponseCompression;
using ROGStrixScopeRx.Library;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR().AddMessagePackProtocol();
builder.Services.AddSingleton<IDatapool, InternalDataPool>();
builder.Services.AddHostedService<DataConsumer>();

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
