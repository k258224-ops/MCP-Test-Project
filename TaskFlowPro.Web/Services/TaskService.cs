using TaskFlowPro.Web.Models;
using TaskFlowPro.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskFlowPro.Web.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync(int userId, string? search = null, TaskFlowPro.Web.Models.TaskStatus? status = null, TaskPriority? priority = null);
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task CreateTaskAsync(TaskItem task);
    Task UpdateTaskAsync(TaskItem task);
    Task DeleteTaskAsync(int id);
}

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasksAsync(int userId, string? search = null, TaskFlowPro.Web.Models.TaskStatus? status = null, TaskPriority? priority = null)
    {
        var query = _context.Tasks.Where(t => t.UserId == userId);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(t => t.Title.Contains(search));

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        return await query.OrderByDescending(t => t.CreatedDate).ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task CreateTaskAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
