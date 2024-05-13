using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Silicon_1.Models;
using System.Text.Json;


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

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscribeViewModel model)
    {
        if (ModelState.IsValid)
        {
            var subscriberEntity = new SubscriberEntity
            {
                Email = model.Email,
                DailyNewsletter = model.DailyNewsletter,
                AdvertisingUpdates = model.AdvertisingUpdates,
                WeekinReview = model.WeekinReview,
                EventUpdates = model.EventUpdates,
                StartupsWeekly = model.StartupsWeekly,
                Podcasts = model.Podcasts,
            };

            var json = JsonSerializer.Serialize(subscriberEntity);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7009/api/Subscriber?key=753fce7a-a9d3-4aa4-8b7e-66926d568e7b", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Status"] = "Account Already Subscribed.";
            }
            else
            {
                TempData["Status"] = "Subscription Updated.";
            }
        }
        else
        {
            TempData["Status"] = "Error; Please Try Again!";
        }

        return RedirectToAction("Index", "Home");
    }
}
