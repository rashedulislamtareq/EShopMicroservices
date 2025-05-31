#region Register Services To The Container

using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add Services To The Container

var assembly = typeof(Program).Assembly;

// Add Carter To Service
builder.Services.AddCarter();

// Add Mediator To Service
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //For MediatR Pipeline Behavior On Fluent Validation
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); //For MediatR Pipeline Behavior On Central Logging
});

// Add Marten To Service As ORM
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

#endregion

#region HTTP Request Pipeline

var app = builder.Build();

//Configure The HTTP Request Pipeline

app.MapCarter();

app.Run();

#endregion