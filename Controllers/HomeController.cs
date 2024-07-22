using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using QuartzNetExample.Models;

namespace QuartzNetExample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISchedulerFactory _schedulerFactory;

    public HomeController(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task<IActionResult> Index()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobDetails = new JobDetailsViewModel
        {
            Jobs = await scheduler.GetCurrentlyExecutingJobs()
        };
        return View(jobDetails);
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
