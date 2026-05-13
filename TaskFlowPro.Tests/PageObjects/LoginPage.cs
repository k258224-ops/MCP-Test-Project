using Microsoft.Playwright;

namespace TaskFlowPro.Tests.PageObjects;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page) => _page = page;

    private ILocator EmailInput => _page.Locator("[data-test='email-input']");
    private ILocator PasswordInput => _page.Locator("[data-test='password-input']");
    private ILocator LoginBtn => _page.Locator("[data-test='login-submit']");
    private ILocator ValidationSummary => _page.Locator(".text-danger");

    public async Task NavigateAsync(string baseUrl) => await _page.GotoAsync($"{baseUrl}/Account/Login");

    public async Task LoginAsync(string email, string password)
    {
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await LoginBtn.ClickAsync();
    }

    public async Task<string> GetErrorMessageAsync() => await ValidationSummary.First.InnerTextAsync();
}
