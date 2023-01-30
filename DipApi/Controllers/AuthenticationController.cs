namespace DipApi.Controllers;

using DipApi.Models;
using DipApi.Entities;
using DipApi.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

[AllowAnonymous]
[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase //безопасность страдает
{
    private readonly ITokenService _tokenService;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public AuthenticationController(ITokenService tokenService, UserManager<User> userManager, 
		SignInManager<User> signInManager)
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

        if (result.Succeeded)
        {
            var token = _tokenService.CreateToken(user);
			var newRefreshToken = _tokenService.GenerateRefreshToken();

			var setresult = await SetRefreshToken(newRefreshToken, user);
			Response.Cookies.Append("email", user.Email);

			if (!setresult.Succeeded)
				return BadRequest(new { message = "Update failed" });

            return Ok(new AuthenticateResponse(user, token));
		}

        return BadRequest(new { message = "Username or password is incorrect" });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
		Response.Cookies.Delete("refreshToken");
		Response.Cookies.Delete("email");

		return Ok("Logout");
    }

	[HttpPost("refresh-token")]
	public async Task<ActionResult<string>> RefreshToken(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);

		var refreshToken = Request.Cookies["refreshToken"];

		if (!user.Token.Equals(refreshToken))
		{
			return Unauthorized("Invalid Refresh Token.");
		}
		if (user.TokenExpires < DateTime.Now)
		{
			return Unauthorized("Token expired.");
		}

		string token = _tokenService.CreateToken(user);
		var newRefreshToken = _tokenService.GenerateRefreshToken();

		var setresult = await SetRefreshToken(newRefreshToken, user);

		if (!setresult.Succeeded)
			return BadRequest(new { message = "Update failed" });

		return Ok(token);
	}

	private async Task<IdentityResult> SetRefreshToken(RefreshToken newRefreshToken, User user)
	{
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Expires = newRefreshToken.Expires
		};
		Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

		user.Token = newRefreshToken.Token;
		user.TokenExpires = newRefreshToken.Expires;
		
		return await _userManager.UpdateAsync(user);
	}
}
