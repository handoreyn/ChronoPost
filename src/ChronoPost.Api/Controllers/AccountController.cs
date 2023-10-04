using ChronoPost.UseCases.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ChronoPost.UseCases.Users.Queries.FindUserById;
using ChronoPost.UseCases.Users.Queries.GenerateJwtToken;
using Microsoft.AspNetCore.Authorization;

namespace ChronoPost.Api.Controllers;

[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _mediator.Send(new FindUserByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand model, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(model, cancellationToken);

        var link = Url.ActionLink("GetUserById", "Account", new { id = result.UserId });
        if (string.IsNullOrEmpty(link)) throw new Exception("User created, but linj with id could not be completed.");
        return Created(link, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpGet("generate-token")]
    [AllowAnonymous]
    public async Task<IActionResult> GetJwtToken([FromForm] GenerateJwtTokenQuery model, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(model, cancellationToken);
        return Ok(result);
    }
}