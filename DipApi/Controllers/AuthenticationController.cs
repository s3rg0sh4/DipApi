namespace DipApi.Controllers;

using DipApi.Models;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DipApi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

[AllowAnonymous]
[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase //безопасность страдает
{
    //private readonly ITokenService _tokenService;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IConfiguration _config;

	public AuthenticationController(/*ITokenService tokenService,*/ UserManager<User> userManager, 
		SignInManager<User> signInManager, IConfiguration configuration)
    {
        //_tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
		_config = configuration;
    }

	[HttpPost]
	public async Task<IActionResult> Login(AuthenticateRequest model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (result.Succeeded)
        {
            var token = CreateToken(user);
			var newRefreshToken = GenerateRefreshToken();

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

		return Ok("Logout");
    }
	
    
    private static RefreshToken GenerateRefreshToken()
	{
		var refreshToken = new RefreshToken
		{
			Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
			Expires = DateTime.Now.AddDays(7).ToUniversalTime()
		};

		return refreshToken;
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

	private string CreateToken(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Email, user.Email)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.Now.AddDays(1),
			signingCredentials: creds);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;
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
		else if (user.TokenExpires < DateTime.Now)
		{
			return Unauthorized("Token expired.");
		}

		string token = CreateToken(user);
		var newRefreshToken = GenerateRefreshToken();

		var setresult = await SetRefreshToken(newRefreshToken, user);

		if (!setresult.Succeeded)
			return BadRequest(new { message = "Update failed" });

		return Ok(token);
	}
}
