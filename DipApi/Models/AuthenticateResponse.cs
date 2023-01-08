namespace WebApi.Models;

using WebApi.Entities;

public class AuthenticateResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Token = token;
    }
}