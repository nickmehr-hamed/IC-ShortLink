using IcFramework.Utilities;
using Microsoft.AspNetCore.Mvc;
using ShortLink.Web.Model;

namespace ShortLink.Web.Controllers;

public class HomeController : Controller
{
    public string BaseUrl { get; }
    public HomeController(IConfiguration configuration) => BaseUrl = configuration["BaseUrl"];


    public IActionResult Index(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return BadRequest();
        using RestApi api = new RestApi().SetUrl(BaseUrl).SetParameters(key);
        ApiResult? result = api.GetAsync<ApiResult>().GetAwaiter().GetResult();
        return result?.IsSuccess ?? false ? Redirect(result.Value.Url) : NotFound();
    }
}