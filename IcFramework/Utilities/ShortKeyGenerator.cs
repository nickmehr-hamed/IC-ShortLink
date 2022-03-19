namespace IcFramework.Utilities;
public static class ShortKeyGenerator
{
    const string _charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public static string Generate(int length = 10)
    {
        Random random = new();
        var result = new string(Enumerable.Repeat(_charSet, length).Select(s => s[random.Next(s.Length)]).ToArray());
        return result;
    }
}
