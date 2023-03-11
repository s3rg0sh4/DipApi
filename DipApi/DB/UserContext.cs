namespace DipApi.DB;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using DipApi.Entities;

#nullable disable

public class UserContext : IdentityDbContext<User>
{
	public UserContext(DbContextOptions<UserContext> options) : base(options) 
	{ 
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		//TODO: вот тут что-то написать

		builder.Entity<User>().HasMany(s => s.FileDetails);
	}

	public DbSet<FileDetails> FileDetails { get; set; }
}