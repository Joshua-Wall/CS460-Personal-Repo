using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Scheduler_Code.Models;

namespace Scheduler_Code.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var model = new ScheduleViewModel { Days = ScheduleCalculator.GetDays(false) };
        return View(model);
    }

    [HttpPost]
    public IActionResult Index(ScheduleViewModel model)
    {
        model.Days = ScheduleCalculator.GetDays(model.IncludeWeekends);
        model.FreeTimes = ScheduleCalculator.CalculateFreeTimes(model.BusyTimes, model.IncludeWeekends);
        model.HasResults = true;
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
