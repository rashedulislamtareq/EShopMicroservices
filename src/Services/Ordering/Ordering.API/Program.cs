var builder = WebApplication.CreateBuilder(args);

// Add Serviceses To The Container


var app = builder.Build();

//Configure The http Request


app.Run();
