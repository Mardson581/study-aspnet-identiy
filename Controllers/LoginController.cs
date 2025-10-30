
using Learn.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Identity.Controllers;

[Route("/{action=Index}")]
public class LoginController : Controller
{
    private readonly IMessageService _service;

    // Logging no ASP.NET é feito através da interface ILogger<categoria>, onde categoria
    // é o nome completo da classe que criará os logs
    private readonly ILogger<LoginController> _logger;

    // Aqui fazemos o IoC e o próprio ASP.NET injeta as dependências
    public LoginController(IMessageService service, ILogger<LoginController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation("Index page was accessed at {DT}", DateTime.Now);
        ViewData["Message"] = _service.GetMessage();
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Devemos checar se o usuário enviou os parâmetros necessários e o ModelState nos ajuda com isso
        if (!ModelState.IsValid)
        {
            // Podemos fazer log de vários níveis como Information, Warning ou Error, por exemplo.
            _logger.LogWarning("Someone tried to login with {{username: {USER}, password: {PASS}}} at {DATE}", username, password, DateTime.Now);
            return BadRequest("This endpoint requires username and password!");
        }

        return Ok("Trouxa!");
    }
}