using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AgentsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAgents()
    {
        return Ok(AgentHub.ConnectedAgents.Keys);
    }
}