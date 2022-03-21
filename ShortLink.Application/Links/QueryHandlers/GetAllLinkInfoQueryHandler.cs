using FluentResults;
using MediatR;
using ShortLink.Application.Links.Queries;
using ShortLink.Persistence.Links.ViewModels;

namespace ShortLink.Application.Links.QueryHandlers;

public class GetAllLinkInfoQueryHandler : IcFramework.Mediator.IRequestHandler<GetAllLinkInfoQuery, IEnumerable<GetAllLinkInfoQueryResponseViewModel>>
{
    public GetAllLinkInfoQueryHandler(IcFramework.Logging.ILogger<GetAllLinkInfoQueryHandler> logger, AutoMapper.IMapper mapper, Persistence.IQueryUnitOfWork unitOfWork) : base()
    {
        Logger = logger;
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    protected AutoMapper.IMapper Mapper { get; }

    protected Persistence.IQueryUnitOfWork UnitOfWork { get; }

    protected IcFramework.Logging.ILogger<GetAllLinkInfoQueryHandler> Logger { get; }

    public async Task<Result<IEnumerable<GetAllLinkInfoQueryResponseViewModel>>> Handle(GetAllLinkInfoQuery request, CancellationToken cancellationToken)
    {
        Result<IEnumerable<GetAllLinkInfoQueryResponseViewModel>> result = new();
        try
        {
            IEnumerable<GetAllLinkInfoQueryResponseViewModel> links = await UnitOfWork.Links.GetAllLinkInfoAsync();
            result.WithValue(links);
        }
        catch (Exception ex)
        {
            Logger.LogError(exception: ex, message: ex.Message);
            result.WithError(errorMessage: ex.Message);
        }
        return result;
    }
}
