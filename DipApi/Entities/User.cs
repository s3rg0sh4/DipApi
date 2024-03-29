namespace DipApi.Entities;

using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

#nullable disable

public class User : IdentityUser
{
	public Guid NaturalPersonGuid { get; set; }
	public string Token { get; set; } = string.Empty;
	public DateTime? TokenExpires { get; set; }
	//ôàéëû
	[ForeignKey("UserId")]
	public virtual ICollection<FileDetails> FileDetails { get; set; }


	public User(): base() { }
	public User(string email) : base(email) 
	{
		Email = email;
	}
}