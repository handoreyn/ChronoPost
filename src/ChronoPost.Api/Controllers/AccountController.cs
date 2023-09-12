using Microsoft.AspNetCore.Mvc;

namespace ChronoPost.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        throw new NotImplementedException();
    }
}