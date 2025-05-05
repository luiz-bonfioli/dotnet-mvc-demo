using Demo.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();
builder.ConfigureLogging();
builder.ConfigureOpenTelemetry();

var app = builder.Build();

app.ConfigureMiddlewares();
app.ConfigureEndpoints();

app.Run();