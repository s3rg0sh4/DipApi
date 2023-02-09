namespace DipApi.Services;

using DipApi.Entities;
using DipApi.Models;

using Microsoft.AspNetCore.Identity;

public interface ITokenService
{
    //string GenerateJwtToken(User user);
	string GenerateRefreshToken();
	string CreateToken(User user);
	Task<IdentityResult> SetRefreshToken(string newRefreshToken, User user);
}
