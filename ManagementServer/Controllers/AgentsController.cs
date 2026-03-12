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

    [HttpGet("{agentName}")]
    public IActionResult GetAgentInfo(string agentName)
    {
        if (AgentHub.AgentInfos.TryGetValue(agentName, out var info))
        {
            return Ok(info);
        }

        return NotFound();
    }
}