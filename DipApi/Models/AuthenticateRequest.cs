namespace DipApi.Models;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}