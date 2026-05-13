using Microsoft.Playwright;

namespace TaskFlowPro.Tests.PageObjects;

public class RegisterPage
{
    private readonly IPage _page;

    public RegisterPage(IPage page) => _page = page;

    private ILocator UsernameInput => _page.Locator("[data-test='username-input']");
    private ILocator EmailInput => _page.Locator("[data-test='email-input']");
    private ILocator PasswordInput => _page.Locator("[data-test='password-input']");
    private ILocator ConfirmPasswordInput => _page.Locator("[data-test='confirm-password-input']");
    private ILocator RegisterBtn => _page.Locator("[data-test='register-submit']");

    public async Task NavigateAsync(string baseUrl) => await _page.GotoAsync($"{baseUrl}/Account/Register");

    public async Task RegisterAsync(string username, string email, string password)
    {
        await UsernameInput.FillAsync(username);
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await ConfirmPasswordInput.FillAsync(password);
        await RegisterBtn.ClickAsync();
    }
}
