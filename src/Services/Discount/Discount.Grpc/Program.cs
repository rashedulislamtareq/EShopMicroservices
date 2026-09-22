using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<DiscountContext>(op =>
        op.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<DiscountService>();


if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.Run();
