using Gridify;
using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.DataAccess.SqlServer;
using WebApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

var configurationSection = builder.Configuration.GetSection("WebApi");
builder.Services.Configure<ApplicationConfiguration>(configurationSection);

ApplicationConfiguration config = new();
configurationSection.Bind(config);

// Console.WriteLine("UPDATED");

if (config.Debug.DangerousLogConfiguration)
{
    var rawConfig = (IConfigurationRoot)builder.Configuration;
    var debugView = rawConfig.GetDebugView();
    Console.WriteLine("Configuration:");
    Console.WriteLine(debugView);
    Console.WriteLine();

    // Console.WriteLine();
    //
    // AnsiConsole.Write(
    //     new Panel(debugView)
    //         .BorderColor(Color.Green)
    //         .Header("Configuration Debug View", Justify.Center)
    //         .Padding(1, 1));
    //
    // Console.WriteLine();
}

GridifyGlobalConfiguration.EnableEntityFrameworkCompatibilityLayer();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(config.SqlServer.ConnectionString));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddPersonTypes();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Api"));
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();