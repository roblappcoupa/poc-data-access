using Gridify;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;
using WebApi.Configuration;
using WebApi.DataAccess.SqlServer;
using WebApi.DependencyInjection;
using WebApi.Utils;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        new ExpressionTemplate(
            "{@t:yyyy-MM-dd HH:mm:ss.fff} [{@l}] {#if SourceContext is not null}[{SourceContext}]{#end} {@m}{#if @x is not null}\n{@p}{#end}\n{@x}\n",
            theme: TemplateTheme.Code
        ))
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

var configurationSection = builder.Configuration.GetSection("WebApi");
builder.Services.Configure<ApplicationConfiguration>(configurationSection);

ApplicationConfiguration config = new();
configurationSection.Bind(config);

if (config.Debug.DangerousLogConfiguration)
{
    var rawConfig = (IConfigurationRoot)builder.Configuration;
    var debugView = rawConfig.GetDebugView();
    Log.Logger.Debug("Configuration:\n{Config}", debugView);
}

builder.Services.AddSerilog(
    x =>
    {
        x.MinimumLevel.Warning().ReadFrom.Configuration(builder.Configuration);
    });

GridifyGlobalConfiguration.EnableEntityFrameworkCompatibilityLayer();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.SqlServer.ConnectionString));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultPersonTypes();
builder.Services.AddInMemoryDataAccess();
builder.Services.AddSqlServerDataAccess();
builder.Services.AddCassandraDataAccess(config.Cassandra);

// FOR ALTERNATIVE REQUEST LOGGING
// builder.Services.AddHttpLogging(
//     options =>
//     {
//         options.RequestHeaders.Add("X-Real-IP");
//         options.RequestHeaders.Add("X-Forwarded-For");
//         options.RequestHeaders.Add("X-Forwarded-Proto");
//     });

var app = builder.Build();

app.AddLifetimeEventHandlers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// FOR ALTERNATIVE REQUEST LOGGING
// app.UseHttpLogging();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Api"));
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();