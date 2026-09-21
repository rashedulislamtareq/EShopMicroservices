using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger services

//Foe Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For Carter
builder.Services.AddCarter();


// For MediatR
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//For Marten
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>();

//For Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";
});

//For Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//For HealthCheck
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    // For Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

//For Carter
app.MapCarter();

//For Global Exception Handler
app.UseExceptionHandler(options => { });

//For HealthCheck UI
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
