namespace DipApi.Services.Impl;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DipApi.Entities;
using DipApi.Helpers;
using DipApi.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

public class TokenService : ITokenService
{
    private readonly AppSettings _appSettings;
	private readonly UserManager<User> _userManager;

    public TokenService(IOptions<AppSettings> appSettings, UserManager<User> userManager)
    {
        _appSettings = appSettings.Value;
		_userManager = userManager;

	}

    //public string GenerateJwtToken(User user)
    //{
    //    // generate token that is valid for 7 days
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
    //        Expires = DateTime.UtcNow.AddDays(7),
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //    };
    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    return tokenHandler.WriteToken(token);
    //}

	public string GenerateRefreshToken()
	{
		return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
	}

	public string CreateToken(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Email, user.Email)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.Now.AddMinutes(int.Parse(_appSettings.TokenValidityInMinutes)),
			signingCredentials: creds);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;
	}

	public async Task<IdentityResult> SetRefreshToken(string newRefreshToken, User user)
	{
		user.Token = newRefreshToken;

		return await _userManager.UpdateAsync(user);
	}
}