using System.Reflection.Emit;

using DipApi.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebApi.Entities;

namespace DipApi.DB
{
	public class UserContext : IdentityDbContext<User>
	{
		public DbSet<Candidate> Candidates { get; set; } = null!;
		public DbSet<HiringApplication> HiringApplications { get; set; } = null!;
		public DbSet<HiringOrder> HiringOrders { get; set; } = null!;
		public DbSet<NaturalPerson> NaturalPersons { get; set; } = null!;
		public DbSet<Subdivision> Subdivisions { get; set; } = null!;

		public UserContext(DbContextOptions<UserContext> options) : base(options) 
		{ 
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			//TODO: вот тут что-то написать
		}
	}
}
