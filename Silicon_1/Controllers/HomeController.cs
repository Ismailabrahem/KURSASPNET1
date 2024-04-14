using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;


namespace Silicon_1.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _dataContext;
    private readonly HttpClient _httpClient;

    public HomeController(DataContext dataContext, IHttpClientFactory httpClientFactory)
    {
        _dataContext = dataContext;
        _httpClient = httpClientFactory.CreateClient();
    }

    public IActionResult Index()
    {
        return View();
    }


}
