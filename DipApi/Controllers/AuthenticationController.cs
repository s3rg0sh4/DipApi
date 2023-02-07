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

			var setresult = await _tokenService.SetRefreshToken(newRefreshToken, user);

			if (!setresult.Succeeded)
				return BadRequest(new { message = "Update failed." });


			return Ok(new AuthenticateResponse(user.Email, token, newRefreshToken));
		}

		return BadRequest("Username or password is incorrect.");
	}

	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();

		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> UpdateToken(AuthenticateResponse model) 
	{ 
		var user = await _userManager.FindByEmailAsync(model.Email);

		if (user.Token == null)
		{
			return Unauthorized("No such refresh token.");
		}
		if (!user.Token.Equals(model.RefreshToken.Token))
		{
			return Unauthorized("Invalid refresh token.");
		}
		if (user.TokenExpires < DateTime.Now)
		{
			return Unauthorized("Token expired.");
		}

		string token = _tokenService.CreateToken(user);
		var newRefreshToken = _tokenService.GenerateRefreshToken();

		var setresult = await _tokenService.SetRefreshToken(newRefreshToken, user);

		if (!setresult.Succeeded)
			return BadRequest(new { message = "Update failed" });

		return Ok(new AuthenticateResponse(user.Email, token, newRefreshToken));
	}
}
