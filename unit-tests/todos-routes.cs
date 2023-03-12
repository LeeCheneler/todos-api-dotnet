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

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        todos.Should().BeEmpty();
    }

    [Fact]
    public async void ShouldAddTodo()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var todo = new Todo { Title = "Test Todo", Description = "Test Description" };
        var result = await client.PostAsJsonAsync("/api/todos", todo);
        var addedTodo = await result.Content.ReadFromJsonAsync<Todo>();

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        addedTodo.Should().BeEquivalentTo(
            new Todo
            {
                Id = 1,
                Title = "Test Todo",
                Description = "Test Description"
            }
        );

        var getTodosResult = await client.GetAsync("/api/todos");
        var todos = await getTodosResult.Content.ReadFromJsonAsync<List<Todo>>();

        getTodosResult.StatusCode.Should().Be(HttpStatusCode.OK);
        todos.Should().BeEquivalentTo(
            new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Title = "Test Todo",
                    Description = "Test Description"
                }
            }
        );
    }

    [Fact]
    public async void ShouldUpdateTodo()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var updatedTodo = new Todo { Id = 1, Title = "Updated Test Todo", Description = "Updated Test Description" };
        var updateResult = await client.PutAsJsonAsync("/api/todos", updatedTodo);
        var updatedTodoResult = await updateResult.Content.ReadFromJsonAsync<Todo>();

        updateResult.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedTodoResult.Should().BeEquivalentTo(
            new Todo
            {
                Id = 1,
                Title = "Updated Test Todo",
                Description = "Updated Test Description"
            }
        );

        var getUpdatedTodosResult = await client.GetAsync("/api/todos");
        var updatedTodos = await getUpdatedTodosResult.Content.ReadFromJsonAsync<List<Todo>>();

        getUpdatedTodosResult.StatusCode.Should().Be(HttpStatusCode.OK);
        updatedTodos.Should().BeEquivalentTo(
            new List<Todo>
            {
                new Todo
                {
                    Id = 1,
                    Title = "Updated Test Todo",
                    Description = "Updated Test Description"
                }
            }
        );
    }
}
