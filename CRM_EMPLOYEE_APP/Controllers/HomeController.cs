using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRM_EMPLOYEE_APP.Models;
using Microsoft.AspNetCore.Authorization;

namespace CRM_EMPLOYEE_APP.Controllers;

[Authorize]
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
