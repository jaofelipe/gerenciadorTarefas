using GerenciadorTarefas.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        [ApiKey]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
