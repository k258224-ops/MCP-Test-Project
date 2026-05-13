using System.ComponentModel.DataAnnotations;

namespace TaskFlowPro.Web.Models;

public enum TaskPriority
{
    Low,
    Medium,
    High
}

public enum TaskStatus
{
    Pending,
    Completed
}

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    public string? ProfilePictureUrl { get; set; }
    
    public List<TaskItem> Tasks { get; set; } = new();
}

public class TaskItem
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DueDate { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}
