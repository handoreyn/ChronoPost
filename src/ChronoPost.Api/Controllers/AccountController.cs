using ChronoPost.UseCases.User.FindUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPost.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new FindUserByIdQuery(id));
        return Ok(result);
    }
}