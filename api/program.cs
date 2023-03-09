using Todos.Api.Services;
using Todos.Api.Routes;
using Todos.Api.Db;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodosDb>(opt => opt.UseInMemoryDatabase("todos"));
builder.Services.AddScoped<TodosService>();

var app = builder.Build();


app.MapGroup("/api").MapTodosApi();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
