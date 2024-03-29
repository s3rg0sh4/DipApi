﻿namespace DipApi.Controllers;

using DipApi.Models;
using DipApi.Entities;
using DipApi.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[AllowAnonymous]
[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
	private readonly ITokenService _tokenService;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public AuthController(ITokenService tokenService, UserManager<User> userManager,
		SignInManager<User> signInManager)
	{
		_tokenService = tokenService;
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[HttpPost]
	public async Task<IActionResult> SetPassword(SetPassword model)
	{
		var user = await _userManager.FindByIdAsync(model.Guid);

		if (user == null)
			return BadRequest("User doesn`t exist.");
			
		if (user.Email != model.Email)
			return BadRequest("Email doesn`t match.");

		if (user.PasswordHash is not null)
			return BadRequest("User password is already set.");

		await _userManager.AddPasswordAsync(user, model.Password);

		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> Login(AuthenticateRequest model)
	{
		User user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
		{
			return BadRequest($"User with email {model.Email} doesn`t exist");
		}

		var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

		if (!result.Succeeded)
			return BadRequest("Password is incorrect.");
			
		var token = _tokenService.CreateToken(user);
		var newRefreshToken = _tokenService.GenerateRefreshToken();
		await _tokenService.SetRefreshToken(newRefreshToken, user);

		return Ok(new AuthenticateResponse(user.Email, token, newRefreshToken));
	}

	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		

		return Ok();
	}

	[HttpPost]
	public async Task<IActionResult> UpdateToken(TokenUpdateRequest model) 
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
		{
			return BadRequest($"User with email {model.Email} doesn`t exist");
		}

		if (user.Token is null)
			return Unauthorized("No such refresh token.");

		if (!user.Token.Equals(model.RefreshToken))
			return Unauthorized("Invalid refresh token.");

		if (user.TokenExpires < DateTime.Now)
			return Unauthorized("Token expired.");

		var token = _tokenService.CreateToken(user);
		var newRefreshToken = _tokenService.GenerateRefreshToken();

		await _tokenService.SetRefreshToken(newRefreshToken, user);

		return Ok(new TokenUpdateResponse(token, newRefreshToken));
	}
}
