using TaskFlowPro.Tests.Helpers;
using TaskFlowPro.Tests.PageObjects;
using NUnit.Framework;
using Microsoft.Playwright;

namespace TaskFlowPro.Tests.Tests;

[TestFixture]
public class TaskTests : BaseTest
{
    private LoginPage _loginPage;
    private TasksPage _tasksPage;

    [SetUp]
    public async Task SetupTasks()
    {
        _loginPage = new LoginPage(Page);
        _tasksPage = new TasksPage(Page);
        
        // Ensure logged in for task tests
        await _loginPage.NavigateAsync(BaseUrl);
        await _loginPage.LoginAsync("john@example.com", "Password123!");
    }

    [Test]
    public async Task CreateTask_ShouldAppearInList()
    {
        string taskTitle = $"Automation Task {Guid.NewGuid().ToString()[..8]}";
        await _tasksPage.NavigateAsync(BaseUrl);
        await _tasksPage.CreateTaskAsync(taskTitle, "High");
        
        await Expect(Page.Locator(".toast-success")).ToBeVisibleAsync();
        await Expect(Page.Locator(".task-list")).ToContainTextAsync(taskTitle);
    }

    [Test]
    public async Task SearchTask_ShouldFilterResults()
    {
        await _tasksPage.NavigateAsync(BaseUrl);
        await _tasksPage.SearchAsync("Setup Playwright");
        
        int count = await _tasksPage.GetTaskCountAsync();
        Assert.That(count, Is.GreaterThanOrEqualTo(1));
        await Expect(Page.Locator(".task-list")).ToContainTextAsync("Setup Playwright");
    }

    [Test]
    public async Task FilterByPriority_ShouldShowCorrectTasks()
    {
        await _tasksPage.NavigateAsync(BaseUrl);
        await _tasksPage.FilterByStatusAsync("Pending");
        
        // Verify all rows have pending status icon (far fa-square)
        var pendingIcons = Page.Locator(".fa-square");
        int count = await pendingIcons.CountAsync();
        int rowCount = await _tasksPage.GetTaskCountAsync();
        
        Assert.That(count, Is.EqualTo(rowCount));
    }
}
