using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagementServer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        [HttpGet("{agentName}")]
        public IActionResult GetConversation(string agentName)
        {
            var messages = ChatStorage.GetConversation(agentName);
            return Ok(messages);
        }
    }
}
