using Microsoft.AspNetCore.Mvc;

namespace ManagementServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly AgentRegistry _registry;

        public AgentsController(AgentRegistry registry)
        {
            _registry = registry;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAgents()
        {
            return Ok(_registry.AgentNames.OrderBy(x => x));
        }

        [HttpGet("{agentName}")]
        public ActionResult<AgentInfo> GetAgent(string agentName)
        {
            var agent = _registry.AgentInfos
                .FirstOrDefault(x => x.MachineName.Equals(agentName, StringComparison.OrdinalIgnoreCase));

            if (agent == null)
                return NotFound();

            return Ok(agent);
        }
    }
}