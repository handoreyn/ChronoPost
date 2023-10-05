using ChronoPost.Core.Services.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ChronoPost.UseCases.Users.Queries.FindUserById;
using ChronoPost.UseCases.Users.Queries.GenerateJwtToken;
using Microsoft.AspNetCore.Authorization;
using ChronoPost.UseCases.Users.Commands.CreateUser;
using ChronoPost.UseCases.Users.Commands.UpdateUser;
using ChronoPost.UseCases.Users.Queries.RefreshJwtToken;

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
    public async Task<IActionResult> Update(int id, UpdateUserCommand model, CancellationToken cancellationToken)
    {
        await _mediator.Send(model, cancellationToken);

        return NoContent();
    }

    [HttpGet("generate-token")]
    [AllowAnonymous]
    public async Task<IActionResult> GetJwtToken([FromForm] GenerateJwtTokenQuery model, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(model, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("refresh-jwt-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshJwtToken([FromBody] TokeModel token, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RefreshJwtTokenQuery(token.Token, User.ToUserClaims().UserId),
            cancellationToken);
        return Ok(result);
    }
}

public record TokeModel(string Token);