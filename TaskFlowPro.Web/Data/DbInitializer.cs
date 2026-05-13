using TaskFlowPro.Web.Models;

namespace TaskFlowPro.Web.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }

        var users = new User[]
        {
            new User { Username = "admin", Email = "admin@taskflow.com", PasswordHash = "Admin123!" },
            new User { Username = "john_doe", Email = "john@example.com", PasswordHash = "Password123!" },
            new User { Username = "jane_smith", Email = "jane@example.com", PasswordHash = "Password123!" }
        };

        foreach (var u in users)
        {
            context.Users.Add(u);
        }
        context.SaveChanges();

        var tasks = new TaskItem[]
        {
            new TaskItem { Title = "Project Kickoff", Description = "Initial meeting with stakeholders", Priority = TaskPriority.High, Status = TaskFlowPro.Web.Models.TaskStatus.Completed, UserId = users[0].Id, CreatedDate = DateTime.UtcNow.AddDays(-5) },
            new TaskItem { Title = "Database Schema Design", Description = "Define tables for TaskFlow Pro", Priority = TaskPriority.High, Status = TaskFlowPro.Web.Models.TaskStatus.Completed, UserId = users[0].Id, CreatedDate = DateTime.UtcNow.AddDays(-4) },
            new TaskItem { Title = "Setup Playwright", Description = "Initialize test project and dependencies", Priority = TaskPriority.Medium, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[1].Id, CreatedDate = DateTime.UtcNow.AddDays(-3) },
            new TaskItem { Title = "Create Login Page", Description = "Design and implement authentication UI", Priority = TaskPriority.Medium, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[1].Id, CreatedDate = DateTime.UtcNow.AddDays(-2) },
            new TaskItem { Title = "Implement CRUD", Description = "Add Create, Read, Update, Delete for tasks", Priority = TaskPriority.High, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[1].Id, CreatedDate = DateTime.UtcNow.AddDays(-1) },
            new TaskItem { Title = "Filter & Search", Description = "Add filtering by status and priority", Priority = TaskPriority.Low, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[2].Id, CreatedDate = DateTime.UtcNow },
            new TaskItem { Title = "Dashboard Stats", Description = "Calculate totals for the dashboard", Priority = TaskPriority.Medium, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[2].Id, CreatedDate = DateTime.UtcNow },
            new TaskItem { Title = "Unit Testing", Description = "Write unit tests for services", Priority = TaskPriority.Low, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[2].Id, CreatedDate = DateTime.UtcNow },
            new TaskItem { Title = "Documentation", Description = "Prepare README and setup guide", Priority = TaskPriority.Medium, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[0].Id, CreatedDate = DateTime.UtcNow },
            new TaskItem { Title = "Final Review", Description = "Review project before submission", Priority = TaskPriority.High, Status = TaskFlowPro.Web.Models.TaskStatus.Pending, UserId = users[0].Id, CreatedDate = DateTime.UtcNow }
        };

        foreach (var t in tasks)
        {
            context.Tasks.Add(t);
        }
        context.SaveChanges();
    }
}
