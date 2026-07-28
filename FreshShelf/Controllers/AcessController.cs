using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshShelf.Controllers;

[ApiController]
[Route("[controller]")]
public class AcessController : ControllerBase
{
        [HttpGet]
        [Authorize(Policy = "MinimumAge")]
        public IActionResult Get()
        {
            return Ok("Acesso permitido!");
        }
}