using TaskFlowPro.Tests.Helpers;
using TaskFlowPro.Tests.PageObjects;
using NUnit.Framework;
using Microsoft.Playwright;

namespace TaskFlowPro.Tests.Tests;

[TestFixture]
public class AuthTests : BaseTest
{
    private LoginPage _loginPage;

    [SetUp]
    public void SetupAuth()
    {
        _loginPage = new LoginPage(Page);
    }

    [Test]
    public async Task ValidLogin_ShouldRedirectToDashboard()
    {
        await _loginPage.NavigateAsync(BaseUrl);
        await _loginPage.LoginAsync("admin@taskflow.com", "Admin123!");
        
        await Expect(Page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex(".*/Home$|.*/$"));
        await Expect(Page.Locator("#page-title")).ToHaveTextAsync("Dashboard");
    }

    [Test]
    public async Task InvalidLogin_ShouldShowErrorMessage()
    {
        await _loginPage.NavigateAsync(BaseUrl);
        await _loginPage.LoginAsync("wrong@example.com", "WrongPass!");
        
        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Does.Contain("Invalid login attempt"));
    }

    [Test]
    public async Task Logout_ShouldRedirectToLogin()
    {
        await _loginPage.NavigateAsync(BaseUrl);
        await _loginPage.LoginAsync("admin@taskflow.com", "Admin123!");
        
        await Page.Locator("#logout-btn").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex(".*/Account/Login$"));
    }
}
