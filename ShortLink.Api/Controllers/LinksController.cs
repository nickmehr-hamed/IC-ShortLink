using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Api.Controllers;

[ApiController]
[Route(template: Infrastructure.Constant.Router.Controller)]
public class LinksController : Infrastructure.ControllerBase
{
    public LinksController(MediatR.IMediator mediator) : base(mediator: mediator)
    {
    }

    #region Post (Create Log)
    [HttpPost]
    [ProducesResponseType(type: typeof(FluentResults.Result<string>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(FluentResults.Result), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Application.Links.Commands.CreateLinkCommand request)
    {
        FluentResults.Result<string>? result = await Mediator.Send(request);
        return FluentResult(result: result);
    }
    #endregion /Post (Create Log)

    #region Get (Get Some Logs)
    [HttpGet(template: "{key}")]
    [ProducesResponseType(type: typeof(FluentResults.Result<Persistence.Links.ViewModels.GetLinkByKeyQueryResponseViewModel>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(FluentResults.Result), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] string key)
    {
        Application.Links.Queries.GetLinkByKeyQuery request = new() { Key = key };
        FluentResults.Result<Persistence.Links.ViewModels.GetLinkByKeyQueryResponseViewModel>? result = await Mediator.Send(request);
        return FluentResult(result: result);
    }
    #endregion /Get (Get Some Logs)
}
