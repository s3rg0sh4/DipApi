namespace DipApi.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
	public Guid NaturalPersonGuid { get; set; }
	public string Token { get; set; } = string.Empty;
	public DateTime TokenExpires { get; set; }
}