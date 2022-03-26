namespace IcFramework.Logging;

public interface ILog
{
    LogLevel Level { get; init; }
    string? Namespace { get; init; }
    string? ClassName { get; init; }
    string? MethodName { get; init; }
    string? RemoteIP { get; init; }
    string? Username { get; init; }
    string? RequestPath { get; init; }
    string? HttpReferrer { get; init; }
    string? Message { get; init; }
    string? Parameters { get; init; }
    string? Exceptions { get; init; }
}
