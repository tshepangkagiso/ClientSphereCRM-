using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRM_EMPLOYEE_APP.Models;

namespace CRM_EMPLOYEE_APP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
