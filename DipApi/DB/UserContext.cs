using System.Reflection.Emit;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebApi.Entities;

namespace DipApi.DB
{
	public class UserContext : IdentityDbContext<User>
	{
		public UserContext(DbContextOptions<UserContext> options) : base(options) 
		{ 
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
