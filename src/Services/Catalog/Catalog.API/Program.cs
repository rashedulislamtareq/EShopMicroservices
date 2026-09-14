var builder = WebApplication.CreateBuilder(args);

//Add Services To The Container

var app = builder.Build();

//Configure The Pipeline
//app.MapGet("/", () => "Hello World!");
app.Run();
