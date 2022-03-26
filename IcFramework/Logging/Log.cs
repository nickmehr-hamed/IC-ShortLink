using System.Text;

namespace IcFramework.Logging;

public class Log : object, ILog
{
    public Log() : base()
    {
    }

    public LogLevel Level { get; init; }
    public string? Namespace { get; init; }
    public string? ClassName { get; init; }
    public string? MethodName { get; init; }
    public string? RemoteIP { get; init; }
    public string? Username { get; init; }
    public string? RequestPath { get; init; }
    public string? HttpReferrer { get; init; }
    public string? Message { get; init; }
    public string? Parameters { get; init; }
    public string? Exceptions { get; init; }

    public override string ToString()
    {
        static string makeTag(string? value, string name) => $"<{ name }>{(string.IsNullOrWhiteSpace(value) ? "NULL" : value)}</{ name }>";

        StringBuilder stringBuilder = new();
        stringBuilder.Append(makeTag(Level.ToString(), nameof(Level)));
        stringBuilder.Append(makeTag(Namespace, nameof(Namespace)));
        stringBuilder.Append(makeTag(ClassName, nameof(ClassName)));
        stringBuilder.Append(makeTag(MethodName, nameof(MethodName)));
        stringBuilder.Append(makeTag(RemoteIP, nameof(RemoteIP)));
        stringBuilder.Append(makeTag(HttpReferrer, nameof(HttpReferrer)));
        stringBuilder.Append(makeTag(Username, nameof(Username)));
        stringBuilder.Append(makeTag(Message, nameof(Message)));
        stringBuilder.Append(makeTag(Exceptions, nameof(Exceptions)));
        stringBuilder.Append(makeTag(Parameters, nameof(Parameters)));
        string result = stringBuilder.ToString();
        return result;
    }
}
