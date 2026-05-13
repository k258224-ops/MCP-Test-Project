using Microsoft.Playwright;

namespace TaskFlowPro.Tests.PageObjects;

public class TasksPage
{
    private readonly IPage _page;

    public TasksPage(IPage page) => _page = page;

    private ILocator CreateTaskBtn => _page.Locator("[data-test='create-task']");
    private ILocator SearchInput => _page.Locator("[data-test='search-input']");
    private ILocator StatusFilter => _page.Locator("[data-test='status-filter']");
    private ILocator PriorityFilter => _page.Locator("[data-test='priority-filter']");
    private ILocator TitleInput => _page.Locator("[data-test='task-title-input']");
    private ILocator SaveBtn => _page.Locator("[data-test='save-task']");
    private ILocator TaskRows => _page.Locator("tr.task-item");

    public async Task NavigateAsync(string baseUrl) => await _page.GotoAsync($"{baseUrl}/Tasks");

    public async Task CreateTaskAsync(string title, string priority = "Medium")
    {
        await CreateTaskBtn.ClickAsync();
        await TitleInput.FillAsync(title);
        await _page.Locator("[data-test='task-priority-select']").SelectOptionAsync(new[] { priority });
        await SaveBtn.ClickAsync();
    }

    public async Task SearchAsync(string query)
    {
        await SearchInput.FillAsync(query);
        await _page.Locator("#filter-btn").ClickAsync();
    }

    public async Task FilterByStatusAsync(string status)
    {
        await StatusFilter.SelectOptionAsync(new[] { status });
        await _page.Locator("#filter-btn").ClickAsync();
    }

    public async Task<int> GetTaskCountAsync() => await TaskRows.CountAsync();
    
    public async Task DeleteTaskAsync(int id)
    {
        await _page.Locator($"[data-test='delete-task-{id}']").ClickAsync();
        // Handle confirm dialog if any (though MVC confirm uses browser default which Playwright handles)
    }
}
