using Learn.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Identity.Controllers;

[ApiController]
[Route("/{action}")]
public class HomeController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signManager;
    private readonly UserManager<ApplicationUser> _userManager;

    // Aqui aplicamos o IoC e o asp.net irá passar as dependências pelo construtor
    // O nome disso é injeção de dependências :-O
    public HomeController(SignInManager<ApplicationUser> signManager, UserManager<ApplicationUser> userManager)
    {
        _signManager = signManager;
        _userManager = userManager;
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Register(string email, string password)
    {
        if (!ModelState.IsValid)
            return BadRequest("This endpoint requires email and password");
        
        ApplicationUser user = new ApplicationUser { UserName = email, Email = email, NormalizedEmail = email.ToUpper() };
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok("User created! Go to /login");
    }

    // Aqui entra a parte de login. Usaremos email e senha como parâmetros
    // Usar ProducesResponseType é nível pleno (eu acho) B-)
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (!ModelState.IsValid)
            return BadRequest("This endpoint requires email and password");

        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Unauthorized("User or password is invalid!");

        var result = await _signManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
            return Unauthorized("User or password is invalid!");
        return Ok("Login successful. Go to /secret");
    }

    // Para um usuário chegar aqui, ele deve estar autenticado, ou seja,
    // Deve ter passado pelo /login
    [Authorize]
    [HttpGet]
    public IActionResult Secret()
    {
        return Ok("Flag{Identity-is-Good!}");
    }
}