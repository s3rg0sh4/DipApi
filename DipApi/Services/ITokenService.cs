namespace DipApi.Services;

using DipApi.Entities;
using DipApi.Models;

using Microsoft.AspNetCore.Identity;

public interface ITokenService
{
    //string GenerateJwtToken(User user);
	RefreshToken GenerateRefreshToken();
	string CreateToken(User user);
	Task<IdentityResult> SetRefreshToken(RefreshToken newRefreshToken, User user);
}
