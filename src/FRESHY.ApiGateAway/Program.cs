using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.MapControllers();
await app.UseOcelot();

app.Run();