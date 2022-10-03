using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shrooms.Presentation.Api.Configurations;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.MapControllerRoutes();

app.Run();
