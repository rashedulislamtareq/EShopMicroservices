
#region Register Services To The Container

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

#endregion

#region HTTP Request Pipeline

var app = builder.Build();

app.MapCarter();

app.Run();

#endregion