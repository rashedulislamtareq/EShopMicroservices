using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

//Add Services To The Container
var assembly = typeof(Program).Assembly;

// For Carter Endpoint
builder.Services.AddCarter();

// For MediatR & Pipeline Behavior
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//For Marten Database Framework
builder.Services.AddMarten(op =>
{
    op.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//For Fluent Validation
builder.Services.AddValidatorsFromAssembly(assembly);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//For Health Cheak UI
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

//For Global Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

if (builder.Environment.IsDevelopment())
    // For Marten Seed data
    builder.Services.InitializeMartenWith<CatalogInitialData>();

var app = builder.Build();

//Configure The Pipeline

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{

    // For Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

// For carter Endpoint
app.MapCarter();

//For Global Exception Handler
app.UseExceptionHandler(op => { });

//For Health Cheak UI
app.UseHealthChecks("/health",
    new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
