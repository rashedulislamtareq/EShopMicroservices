using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Grpc Service
builder.Services.AddGrpc();

// Add Sqlite Database Service
builder.Services.AddDbContext<DiscountContext>(op =>
    op.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.

// Added Extension Method To Migrate Database At Application Startup
app.UseMigration();

app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "");

app.Run();
