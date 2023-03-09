using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Todos.Api.Models;


namespace Todos.Api.UnitTests;

public class TodosRoutesTests
{
  [Fact]
  public async void ShouldReturnEmptyTodos()
  {
    await using var application = new WebApplicationFactory<Program>();
    using var client = application.CreateClient();

    var result = await client.GetAsync("/api/todos");
    var todos = await result.Content.ReadFromJsonAsync<List<Todo>>();

    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    Assert.IsAssignableFrom<List<Todo>>(todos);
    Assert.NotNull(todos);
    Assert.Empty(todos);
  }

  [Fact]
  public async void ShouldAddTodo()
  {
    await using var application = new WebApplicationFactory<Program>();
    using var client = application.CreateClient();

    var todo = new Todo { Title = "Test Todo", Description = "Test Description" };
    var result = await client.PostAsJsonAsync("/api/todos", todo);
    var addedTodo = await result.Content.ReadFromJsonAsync<Todo>();

    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    Assert.IsAssignableFrom<Todo>(addedTodo);
    Assert.NotNull(addedTodo);
    Assert.Equal(todo.Title, addedTodo.Title);
    Assert.Equal(todo.Description, addedTodo.Description);

    var getTodosResult = await client.GetAsync("/api/todos");
    var todos = await getTodosResult.Content.ReadFromJsonAsync<List<Todo>>();

    Assert.Equal(HttpStatusCode.OK, getTodosResult.StatusCode);
    Assert.IsAssignableFrom<List<Todo>>(todos);
    Assert.NotNull(todos);
    Assert.Equal(1, todos.Count);
    Assert.Equal(todo.Title, todos[0].Title);
    Assert.Equal(todo.Description, todos[0].Description);
  }

  [Fact]
  public async void ShouldUpdateTodo()
  {
    await using var application = new WebApplicationFactory<Program>();
    using var client = application.CreateClient();

    var updatedTodo = new Todo { Id = 1, Title = "Updated Test Todo", Description = "Updated Test Description" };
    var updateResult = await client.PutAsJsonAsync("/api/todos", updatedTodo);
    var updatedTodoResult = await updateResult.Content.ReadFromJsonAsync<Todo>();

    Assert.Equal(HttpStatusCode.OK, updateResult.StatusCode);
    Assert.IsAssignableFrom<Todo>(updatedTodoResult);
    Assert.NotNull(updatedTodoResult);
    Assert.Equal(updatedTodo.Id, updatedTodoResult.Id);
    Assert.Equal(updatedTodo.Title, updatedTodoResult.Title);
    Assert.Equal(updatedTodo.Description, updatedTodoResult.Description);

    var getUpdatedTodosResult = await client.GetAsync("/api/todos");
    var updatedTodos = await getUpdatedTodosResult.Content.ReadFromJsonAsync<List<Todo>>();

    Assert.Equal(HttpStatusCode.OK, getUpdatedTodosResult.StatusCode);
    Assert.IsAssignableFrom<List<Todo>>(updatedTodos);
    Assert.NotNull(updatedTodos);
    Assert.Equal(1, updatedTodos.Count);
    Assert.Equal(updatedTodo.Id, updatedTodos[0].Id);
    Assert.Equal(updatedTodo.Title, updatedTodos[0].Title);
    Assert.Equal(updatedTodo.Description, updatedTodos[0].Description);
  }
}