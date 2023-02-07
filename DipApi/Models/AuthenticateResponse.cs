namespace DipApi.Models;

using DipApi.Entities;

public class AuthenticateResponse
{
    public string Email { get; set; }
    public string Token { get; set; }
    public RefreshToken RefreshToken { get; set; }

    public AuthenticateResponse(string email, string token, RefreshToken refreshToken)
    {
        Email = email;
        Token = token;
        RefreshToken = refreshToken;
    }
}