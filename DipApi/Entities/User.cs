namespace DipApi.Entities;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using NpgsqlTypes;

public class User : IdentityUser
{
	public Guid NaturalPersonGuid { get; set; }
	public string Token { get; set; } = string.Empty;
	public DateTime TokenExpires { get; set; }
}