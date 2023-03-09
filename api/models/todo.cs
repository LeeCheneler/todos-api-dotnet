using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.Api.Models;

public enum TodoStatus
{
  NotStarted,
  InProgress,
  Completed
}

public class Todo
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string Title { get; set; } = String.Empty;

  public string? Description { get; set; }

  public TodoStatus Status { get; set; } = TodoStatus.NotStarted;
}
