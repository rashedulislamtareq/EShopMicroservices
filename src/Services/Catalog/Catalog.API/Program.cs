
#region Register Services To The Container

var builder = WebApplication.CreateBuilder(args);

#endregion

#region HTTP Request Pipeline

var app = builder.Build();

app.Run();

#endregion