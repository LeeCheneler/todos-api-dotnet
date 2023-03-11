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

      return TypedResults.Ok(todos);
    });

    group.MapPost("/todos", async (Todo todo, TodosService todosService) =>
    {
      var addedTodo = await todosService.AddTodoAsync(todo);

      return TypedResults.Ok(addedTodo);
    });

    group.MapPut("/todos", async (Todo todo, TodosService todosService) =>
    {
      var updatedTodo = await todosService.UpdateTodoAsync(todo);

      return TypedResults.Ok(updatedTodo);
    });

    return group;
  }
}
