using Microsoft.AspNetCore.Routing;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Routes;

public static class RouteGroupBuilderExtensions
{
  public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group)
  {
    group.MapGet("/todos", async (TodosService todosService) =>
    {
      var todos = await todosService.GetTodosAsync();

      return Results.Ok(todos);
    });

    group.MapPost("/todos", async (Todo todo, TodosService todosService) =>
    {
      var addedTodo = await todosService.AddTodoAsync(todo);

      return Results.Ok(addedTodo);
    });

    group.MapPut("/todos", async (Todo todo, TodosService todosService) =>
    {
      var updatedTodo = await todosService.UpdateTodoAsync(todo);

      return Results.Ok(updatedTodo);
    });


    return group;
  }
}
