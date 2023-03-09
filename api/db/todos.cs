using Microsoft.EntityFrameworkCore;
using Todos.Api.Models;

namespace Todos.Api.Db;


public class TodosDb : DbContext
{
  public TodosDb(DbContextOptions<TodosDb> options)
        : base(options) { }

  public DbSet<Todo> Todos => Set<Todo>();
}
