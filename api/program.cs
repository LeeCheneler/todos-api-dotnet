using Todos.Api.Services;
using Todos.Api.Routes;
using Todos.Api.Db;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodosDb>(opt => opt.UseInMemoryDatabase("todos"));
builder.Services.AddScoped<TodosService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/api").MapTodosApi(app.Logger).WithOpenApi();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
