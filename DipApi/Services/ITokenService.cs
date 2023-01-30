namespace DipApi.Services;
using DipApi.Entities;
using DipApi.Models;

public interface ITokenService
{
    string GenerateJwtToken(User user);
	public RefreshToken GenerateRefreshToken();
	public string CreateToken(User user);
}
