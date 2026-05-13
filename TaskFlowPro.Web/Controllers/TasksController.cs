using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskFlowPro.Web.Models;
using TaskFlowPro.Web.Services;

namespace TaskFlowPro.Web.Controllers;

[Authorize]
public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IActionResult> Index(string? search, TaskFlowPro.Web.Models.TaskStatus? status, TaskPriority? priority)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tasks = await _taskService.GetTasksAsync(userId, search, status, priority);
        
        ViewBag.Search = search;
        ViewBag.Status = status;
        ViewBag.Priority = priority;
        
        return View(tasks);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TaskItem());
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            task.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            task.CreatedDate = DateTime.UtcNow;
            await _taskService.CreateTaskAsync(task);
            TempData["SuccessMessage"] = "Task created successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null || task.UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            return NotFound();
            
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            var existingTask = await _taskService.GetTaskByIdAsync(task.Id);
            if (existingTask == null || existingTask.UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
                return NotFound();

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.Priority = task.Priority;
            existingTask.DueDate = task.DueDate;

            await _taskService.UpdateTaskAsync(existingTask);
            TempData["SuccessMessage"] = "Task updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null || task.UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            return NotFound();

        await _taskService.DeleteTaskAsync(id);
        TempData["SuccessMessage"] = "Task deleted successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null || task.UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            return NotFound();

        task.Status = task.Status == TaskFlowPro.Web.Models.TaskStatus.Pending ? TaskFlowPro.Web.Models.TaskStatus.Completed : TaskFlowPro.Web.Models.TaskStatus.Pending;
        await _taskService.UpdateTaskAsync(task);
        return RedirectToAction(nameof(Index));
    }
}
