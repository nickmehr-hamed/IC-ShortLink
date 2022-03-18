namespace IcFramework.Mediator
{
    public class Query<TValue> :MediatR.IRequest<FluentResults.Result<TValue>>
    {
        public Query() : base()
        {
        }
    }
}
