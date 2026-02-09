using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private static readonly List<ChatMessage> Messages = new();

    [HttpPost("send")]
    public IActionResult SendMessage([FromBody] ChatMessage message)
    {
        Messages.Add(message);
        return Ok();
    }

    [HttpGet("get")]
    public ActionResult<List<ChatMessage>> GetMessages()
    {
        return Messages.OrderBy(m => m.Timestamp).ToList();
    }
}