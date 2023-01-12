using System.Data;

using DipApi.DB;
using DipApi.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

{
	var services = builder.Services;
	var connectionStringUsers = builder.Configuration.GetConnectionString("DefaultConnection");

	services.AddCors();
	services.AddControllers();
	services.AddDbContext<UserContext>(options => options.UseNpgsql(connectionStringUsers));
	services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserContext>();

	// configure strongly typed settings object
	services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

	// configure DI for application services
	services.AddScoped<ICandidateService, CandidateService>();
	services.AddScoped<IHiringApplicationService, HiringApplicationService>();
	services.AddScoped<IHiringOrderService, HiringOrderService>();
	services.AddScoped<INaturalPersonService, NaturalPersonService>();
	services.AddScoped<ITokenService, TokenService>();

	services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
		.AddJwtBearer();
	services.AddAuthorization();
}

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseCors(x => x
		.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run("http://localhost:4000");