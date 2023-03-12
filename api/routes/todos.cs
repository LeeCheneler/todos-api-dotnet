using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Routes;

public static class RouteGroupBuilderExtensions
{
  public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group, ILogger logger)
  {
    group.MapGet("/todos", async (TodosService todosService) =>
    {
      logger.LogInformation("Getting all todos");

      var todos = await todosService.GetTodosAsync();

      return TypedResults.Ok(todos);
    });

    group.MapPost("/todos", async (Todo todo, TodosService todosService) =>
    {
      logger.LogInformation("Adding todo");

      var addedTodo = await todosService.AddTodoAsync(todo);

      return TypedResults.Ok(addedTodo);
    });

    group.MapPut("/todos", async (Todo todo, TodosService todosService) =>
    {
      logger.LogInformation("Updating todo");

      var updatedTodo = await todosService.UpdateTodoAsync(todo);

      return TypedResults.Ok(updatedTodo);
    });

    return group;
  }
}
