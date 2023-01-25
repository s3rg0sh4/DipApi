namespace WebApi.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
	public Guid? NaturalPersonGuid { get; set; } = Guid.Empty;
}