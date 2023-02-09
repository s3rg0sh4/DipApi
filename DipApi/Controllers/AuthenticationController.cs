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

	//TODO: сделать 2 ручки
	//одна принимает почту и генерит кусок ссылки
	//вторая ставит пароль по этой ссылке

	[HttpPost]
	public async Task<IActionResult> Register(RegisterDirectum model)
	{
		var user = new User();
		await _userManager.CreateAsync(user);


		return Ok();
	}
	

	[HttpPost]
	public async Task<IActionResult> Login(AuthenticateRequest model)
	{
		User user;
		Microsoft.AspNetCore.Identity.SignInResult result;
		try
		{
			user = await _userManager.FindByEmailAsync(model.Email);
			result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
		}
		catch (Exception) 
		{
			return BadRequest("User doesn`t exist");
		}



		if (result.Succeeded)
		{
			var token = _tokenService.CreateToken(user);
			var newRefreshToken = _tokenService.GenerateRefreshToken();

			var setresult = await _tokenService.SetRefreshToken(newRefreshToken, user);

			if (!setresult.Succeeded)
				return BadRequest(new { message = "Update failed." });


			return Ok(new AuthenticateResponse(user.Email, token, newRefreshToken));
		}

		return BadRequest("Password is incorrect.");
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
		if (!user.Token.Equals(model.RefreshToken))
		{
			return Unauthorized("Invalid refresh token.");
		}

		string token = _tokenService.CreateToken(user);
		var newRefreshToken = _tokenService.GenerateRefreshToken();

		var setresult = await _tokenService.SetRefreshToken(newRefreshToken, user);

		if (!setresult.Succeeded)
			return BadRequest(new { message = "Update failed" });

		return Ok(new AuthenticateResponse(user.Email, token, newRefreshToken));
	}
}
