
#region Register Services To The Container

using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add Mediator To Service
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //For MediatR Pipeline Behavior
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

#endregion

#region HTTP Request Pipeline

var app = builder.Build();

app.MapCarter();

app.Run();

#endregion