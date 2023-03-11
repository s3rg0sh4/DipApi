namespace DipApi.Services.Implementation;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DipApi.Entities;
using DipApi.Helpers;
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
		user.TokenExpires = DateTime.Now.AddDays(int.Parse(_appSettings.RefreshTokenValidityInDays)).ToUniversalTime();

		return await _userManager.UpdateAsync(user);
	}
}