using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MistyDroid.Server.Services;

namespace MistyDroid.Server.Controllers;

[Route("api")]
[ApiController]
public class SessionController(AdbManager adbManager) : ControllerBase
{
    [HttpGet("session/{sessionName}/screenshot/{screenshotId}")]
    public async Task<FileResult> Screenshot(string sessionName, string screenshotId)
    {
        var session = adbManager.GetOrCreateSession(sessionName);

        var screenshot = await session.Screenshot();

        return File(screenshot, "image/png");
    }

    [HttpPost("session/{sessionName}/click")]
    public async Task<object> Click(string sessionName, [FromBody]ClickInfo clickInfo)
    {
        var session = adbManager.GetOrCreateSession(sessionName);
        await session.Click(clickInfo.Point);

        return new { };
    }
}

public class ClickInfo
{
    public required Point Point { get; init; }
}

