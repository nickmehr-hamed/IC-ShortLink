using IcFramework.Utilities;

namespace ShortLink.Application.Links.CommandHandlers;

public class CreateLogCommandHandler : IcFramework.Mediator.IRequestHandler<Commands.CreateLinkCommand, string>
{
    public CreateLogCommandHandler(IcFramework.Logging.ILogger<CreateLogCommandHandler> logger, AutoMapper.IMapper mapper, Persistence.IUnitOfWork unitOfWork) : base()
    {
        Logger = logger;
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }

    protected AutoMapper.IMapper Mapper { get; }

    protected Persistence.IUnitOfWork UnitOfWork { get; }

    protected IcFramework.Logging.ILogger<CreateLogCommandHandler> Logger { get; }

    public async Task<FluentResults.Result<string>> Handle(Commands.CreateLinkCommand request, CancellationToken cancellationToken)
    {
        FluentResults.Result<string>? result = new ();

        try
        {
            var link = Mapper.Map<Domain.Models.Link>(source: request);
            link.ShortKey = ShortKeyGenerator.Generate();
            await UnitOfWork.Links.InsertAsync(link);
            await UnitOfWork.SaveAsync();
            result.WithValue(value: link.ShortKey);
            string successInsert = string.Format(Resources.Messages.SuccessInsert, nameof(Domain.Models.Link));
            result.WithSuccess(successMessage: successInsert);
        }
        catch (Exception ex)
        {
            Logger.LogError(exception: ex, message: ex.Message);
            result.WithError(errorMessage: ex.Message);
        }
        return result;
    }
}
