#region Register Services To The Container

var builder = WebApplication.CreateBuilder(args);

// Add Services To The Container

#endregion

#region HTTP Request Pipeline

var app = builder.Build();

//Configure The HTTP Request Pipeline

app.Run();

#endregion