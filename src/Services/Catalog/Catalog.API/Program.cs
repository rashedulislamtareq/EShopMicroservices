
#region Register Services To The Container

using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add Mediator To Service
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //For MediatR Pipeline Behavior On Fluent Validation
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); //For MediatR Pipeline Behavior On Central Logging
});

// Add Carter To Service
builder.Services.AddCarter();

// Add Fluent Validator To Service
builder.Services.AddValidatorsFromAssembly(assembly);

// Add Marten To Service As ORM
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// Seed Data With Martin, Only In Development Environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

// Add Custom ExceptionHandler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add Healthcheck Service
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

#endregion

#region HTTP Request Pipeline

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { }); //Empty Options Indicates Custom Exception Handler

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

#endregion