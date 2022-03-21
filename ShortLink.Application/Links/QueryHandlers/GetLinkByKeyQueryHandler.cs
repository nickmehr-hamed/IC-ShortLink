using FluentResults;
using ShortLink.Application.Links.Queries;
using ShortLink.Persistence.Links.ViewModels;

namespace ShortLink.Application.Links.QueryHandlers;

public class GetLinkByKeyQueryHandler : IcFramework.Mediator.IRequestHandler<GetLinkByKeyQuery, GetLinkByKeyQueryResponseViewModel>
{
    public GetLinkByKeyQueryHandler(IcFramework.Logging.ILogger<GetLinkByKeyQueryHandler> logger, AutoMapper.IMapper mapper, Persistence.IQueryUnitOfWork unitOfWork) : base()
    {
        Logger = logger;
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    protected AutoMapper.IMapper Mapper { get; }

    protected Persistence.IQueryUnitOfWork UnitOfWork { get; }

    protected IcFramework.Logging.ILogger<GetLinkByKeyQueryHandler> Logger { get; }


    public async Task<Result<GetLinkByKeyQueryResponseViewModel>> Handle(GetLinkByKeyQuery request, CancellationToken cancellationToken)
    {
        Result<GetLinkByKeyQueryResponseViewModel>? result = new();
        try
        {
            GetLinkByKeyQueryResponseViewModel? link = await UnitOfWork.Links.GetLinkByKeyAsync(request.Key);
            if (link != null)
                result.WithValue(link);
        }
        catch (Exception ex)
        {
            Logger.LogError(exception: ex, message: ex.Message);
            result.WithError(errorMessage: ex.Message);
        }
        return result;
    }
}
