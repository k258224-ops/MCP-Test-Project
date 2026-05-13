using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskFlowPro.Web.Models;
using TaskFlowPro.Web.Services;
using TaskFlowPro.Web.ViewModels;

namespace TaskFlowPro.Web.Controllers;

public class HomeController : Controller
{
    private readonly ITaskService _taskService;

    public HomeController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return View("Landing");
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString)) return View("Landing");

        var userId = int.Parse(userIdString);
        var tasks = await _taskService.GetTasksAsync(userId);

        var model = new DashboardViewModel
        {
            TotalTasks = tasks.Count,
            CompletedTasks = tasks.Count(t => t.Status == TaskFlowPro.Web.Models.TaskStatus.Completed),
            PendingTasks = tasks.Count(t => t.Status == TaskFlowPro.Web.Models.TaskStatus.Pending),
            RecentTasks = tasks.Take(5).ToList()
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
