var builder = WebApplication.CreateBuilder(args);

//Add Services To The Container

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure The Pipeline

app.Run();
