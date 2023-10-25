using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using random_passcode.Models;

namespace random_passcode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View("Index");
    }

    [HttpPost]
    public IActionResult Generate(string generateCode)
    {
        int passCode = HttpContext.Session.GetInt32("PassCode") ?? 0;

        if (generateCode == "start")
        {
            string randomPassword = GenerateRandomPassword(14);
            passCode += 1;

            HttpContext.Session.SetString("RandomPassword", randomPassword);
            HttpContext.Session.SetInt32("PassCode", passCode);
        }

        return RedirectToAction("Index");
    }

    private string GenerateRandomPassword(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var password = new System.Text.StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            password.Append(chars[random.Next(chars.Length)]);
        }

        return password.ToString();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
