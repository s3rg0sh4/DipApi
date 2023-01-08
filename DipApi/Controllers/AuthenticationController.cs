namespace WebApi.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

using WebApi.Models;
using WebApi.Services;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

    public AuthenticationController(ITokenService tokenService,
        UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(AuthenticateRequest model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        // authentication successful so generate jwt token
        if (result.Succeeded)
        {
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new AuthenticateResponse(user, token));
        }

        return BadRequest(new { message = "Username or password is incorrect" });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logout");
    }
}
