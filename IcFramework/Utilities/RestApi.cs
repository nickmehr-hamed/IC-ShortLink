using System.Text.Json;

namespace IcFramework.Utilities;
public class RestApi : IDisposable
{
    public RestApi()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        Client = new HttpClient(clientHandler);
    }
    private HttpClient Client { get; }
    public string? Parameters { get; private set; }

    public RestApi SetUrl(string url)
    {
        Client.BaseAddress = new Uri(url);
        return this;
    }

    public RestApi SetParameters(string parameters)
    {
        Parameters = parameters;
        return this;
    }

    public async Task<T> GetAsync<T>()
    {
        T result = default(T);
        HttpResponseMessage response = await Client.GetAsync(Parameters);
        if (response.IsSuccessStatusCode)
        {
            string responseValue = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            result = JsonSerializer.Deserialize<T>(responseValue, options);
        }
        return result;
    }

    public void Dispose() => Client.Dispose();
}
