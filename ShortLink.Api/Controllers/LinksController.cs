using Microsoft.AspNetCore.Mvc;

namespace ShortLink.Api.Controllers;

[ApiController]
[Route(template: Infrastructure.Constant.Router.Controller)]
public class LinksController : Infrastructure.ControllerBase
{
    public LinksController(MediatR.IMediator mediator) : base(mediator: mediator)
    {
    }

    #region Post (Create New ShortLink)
    /// <summary>
    /// gets a Url, Title, OwnerId; Generates a new ShortKey and Insert the record in database and returns ShortKey as result
    /// </summary>
    [HttpPost]
    [ProducesResponseType(type: typeof(FluentResults.Result<string>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(FluentResults.Result), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Application.Links.Commands.CreateLinkCommand request)
    {
        FluentResults.Result<string>? result = await Mediator.Send(request);
        return FluentResult(result: result);
    }
    #endregion /Post (Create New ShortLink)

    #region Get (Get A ShortLink Url and Title By Short Key)
    /// <summary>
    /// gets ShortKey and fetch the Url info, it returns Url, Title as result
    /// </summary>
    [HttpGet(template: "{key}")]
    [ProducesResponseType(type: typeof(FluentResults.Result<Persistence.Links.ViewModels.GetLinkByKeyQueryResponseViewModel>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(FluentResults.Result), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromRoute] string key)
    {
        Application.Links.Queries.GetLinkByKeyQuery request = new() { Key = key };
        FluentResults.Result<Persistence.Links.ViewModels.GetLinkByKeyQueryResponseViewModel>? result = await Mediator.Send(request);
        return FluentResult(result: result);
    }
    #endregion /Get (Get A ShortLink Url and Title By Short Key)
}
