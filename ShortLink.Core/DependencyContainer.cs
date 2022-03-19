using FluentValidation;
using IcFramework.Mediator;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShortLink.Core;

public static class DependencyContainer
{
    static DependencyContainer()
    {
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(serviceType: typeof(IcFramework.Logging.ILogger<>), implementationType: typeof(IcFramework.Logging.NLogAdapter<>));

        services.AddMediatR(typeof(Application.MediatorEntryPoint));
        services.AddValidatorsFromAssembly(typeof(Application.MediatorEntryPoint).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddAutoMapper(profileAssemblyMarkerTypes: typeof(Application.MediatorEntryPoint));

        services.AddTransient<Persistence.IUnitOfWork, Persistence.UnitOfWork>(current =>
        {
            string databaseConnectionString = configuration.GetSection(key: "ConnectionStrings").GetSection(key: "CommandsConnectionString").Value;
            string databaseProviderString = configuration.GetSection(key: "CommandsDatabaseProvider").Value;

            IcFramework.Persistence.Enums.Provider databaseProvider = (IcFramework.Persistence.Enums.Provider)Convert.ToInt32(databaseProviderString);
            IcFramework.Persistence.Options options = new IcFramework.Persistence.Options { Provider = databaseProvider, ConnectionString = databaseConnectionString, };
            return new Persistence.UnitOfWork(options: options);
        });

        services.AddTransient<Persistence.IQueryUnitOfWork, Persistence.QueryUnitOfWork>(current =>
        {
            string databaseConnectionString = configuration.GetSection(key: "ConnectionStrings").GetSection(key: "QueriesConnectionString").Value;
            string databaseProviderString = configuration.GetSection(key: "QueriesDatabaseProvider").Value;

            IcFramework.Persistence.Enums.Provider databaseProvider = (IcFramework.Persistence.Enums.Provider)Convert.ToInt32(databaseProviderString);
            IcFramework.Persistence.Options options = new IcFramework.Persistence.Options { Provider = databaseProvider, ConnectionString = databaseConnectionString, };
            return new Persistence.QueryUnitOfWork(options: options);
        });
    }
}
