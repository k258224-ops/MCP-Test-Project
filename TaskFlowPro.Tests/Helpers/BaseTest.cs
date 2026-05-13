using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;

namespace TaskFlowPro.Tests.Helpers;

public class BaseTest : PageTest
{
    protected string BaseUrl = "https://localhost:7036/"; // Default dev port, adjust if needed

    [SetUp]
    public async Task Setup()
    {
        // Add any global setup here
        await Context.Tracing.StartAsync(new()
        {
            Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task TearDown()
    {
        var result = TestContext.CurrentContext.Result.Outcome.Status;

        if (result == TestStatus.Failed)
        {
            var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            await Page.ScreenshotAsync(new() { Path = path });
            TestContext.AddTestAttachment(path);
        }

        await Context.Tracing.StopAsync(new()
        {
            Path = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Traces", $"{TestContext.CurrentContext.Test.Name}.zip")
        });
    }
}
