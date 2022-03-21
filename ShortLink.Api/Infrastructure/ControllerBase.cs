using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Api.Infrastructure;

public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected ControllerBase(MediatR.IMediator mediator) : base() => Mediator = mediator;

    protected MediatR.IMediator Mediator { get; }

    protected IActionResult FluentResult<T>(FluentResults.Result<T> result) 
        => result.IsSuccess ? Ok(value: result) : BadRequest(error: result.ToResult());
}
