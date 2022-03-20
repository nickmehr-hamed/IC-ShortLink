using System.Text;

namespace IcFramework.Logging;

public class Log : object, ILog
{
    public Log() : base()
    {
    }

    public LogLevel Level { get; set; }
    public string? Namespace { get; set; }
    public string? ClassName { get; set; }
    public string? MethodName { get; set; }
    public string? RemoteIP { get; set; }
    public string? Username { get; set; }
    public string? RequestPath { get; set; }
    public string? HttpReferrer { get; set; }
    public string? Message { get; set; }
    public string? Parameters { get; set; }
    public string? Exceptions { get; set; }

    public override string ToString()
    {
        string MakeTag(string? value, string name)
        => $"<{ name }>{(string.IsNullOrWhiteSpace(value) ? "NULL" : value)}</{ name }>";

        StringBuilder stringBuilder = new();
        stringBuilder.Append(MakeTag(Level.ToString(), nameof(Level)));
        stringBuilder.Append(MakeTag(Namespace, nameof(Namespace)));
        stringBuilder.Append(MakeTag(ClassName, nameof(ClassName)));
        stringBuilder.Append(MakeTag(MethodName, nameof(MethodName)));
        stringBuilder.Append(MakeTag(RemoteIP, nameof(RemoteIP)));
        stringBuilder.Append(MakeTag(HttpReferrer, nameof(HttpReferrer)));
        stringBuilder.Append(MakeTag(Username, nameof(Username)));
        stringBuilder.Append(MakeTag(Message, nameof(Message)));
        stringBuilder.Append(MakeTag(Exceptions, nameof(Exceptions)));
        stringBuilder.Append(MakeTag(Parameters, nameof(Parameters)));
        string result = stringBuilder.ToString();
        return result;
    }
}
