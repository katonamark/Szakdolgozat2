using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementServer.Controllers
{
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

        [HttpGet("agent/{agentName}")]
        public IActionResult GetConversationForAgent(string agentName)
        {
            var messages = ChatStorage.GetConversation(agentName);
            return Ok(messages);
        }
    }
}