using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    // For Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();

    //Extension Method For seed Data
    await app.InitialiseDatabaseAsync();
}

app.Run();
