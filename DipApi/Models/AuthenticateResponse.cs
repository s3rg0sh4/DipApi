namespace DipApi.Models;

using DipApi.Entities;

public class AuthenticateResponse
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public AuthenticateResponse(string email, string token, string refreshToken)
    {
        Email = email;
        Token = token;
        RefreshToken = refreshToken;
    }
}