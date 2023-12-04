using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Module32.Models;
using Module32.Models.Db;

namespace Module32.Controllers;

public class HomeController : Controller
{
    private readonly IBlogRepository _repo;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
    {
        _logger = logger;
        _repo = repo;
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