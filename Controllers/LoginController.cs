
using Learn.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Identity.Controllers;

[Route("/{action=Index}")]
public class LoginController : Controller
{
    private readonly IMessageService _service;
    public LoginController(IMessageService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Message"] = _service.GetMessage();
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        return Ok("Trouxa!");
    }
}