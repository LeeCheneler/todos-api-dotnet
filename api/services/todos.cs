using Microsoft.EntityFrameworkCore;
using Todos.Api.Db;
using Todos.Api.Models;

namespace Todos.Api.Services;

public class TodosService
{
  private readonly TodosDb _db;

  public TodosService(TodosDb db)
  {
    _db = db;
  }

  public async Task<List<Todo>> GetTodosAsync()
  {
    return await _db.Todos.AsQueryable().ToListAsync();
  }

  public async Task<Todo> AddTodoAsync(Todo todo)
  {
    await _db.Todos.AddAsync(todo);
    await _db.SaveChangesAsync();

    return todo;
  }

  public async Task<Todo> UpdateTodoAsync(Todo todo)
  {
    _db.Todos.Update(todo);
    await _db.SaveChangesAsync();

    return todo;
  }
}
