using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MistyDroid.Server.Services;

namespace MistyDroid.Server.Controllers;

[Route("api")]
[ApiController]
public class SessionController(AdbManager adbManager) : ControllerBase
{
    [HttpGet("session/{sessionName}/screenshot")]
    public async Task<FileResult> Screenshot(string sessionName)
    {
        var session = adbManager.GetOrCreateSession(sessionName);

        var screenshot = await session.Screenshot();

        return File(screenshot, "image/png");
    }
}
