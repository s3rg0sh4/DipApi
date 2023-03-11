using System.Text;

using DipApi.DB;
using DipApi.Services;
using DipApi.Entities;
using DipApi.Helpers;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using DipApi.Services.Implementation;

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
	services.AddDbContext<UserContext>(options => options.UseLazyLoadingProxies().UseNpgsql(connectionStringUsers));
	services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserContext>();

	// configure strongly typed settings object
	services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

	// configure DI for application services
	services.AddScoped<INaturalPersonService, NaturalPersonService>();
	services.AddScoped<ITokenService, TokenService>();
	services.AddScoped<IEmailService, EmailService>();
	services.AddScoped<IStatusService, StatusService>();
	services.AddScoped<IRateService, RateService>();
	services.AddScoped<IFileService, FileService>();

	services.AddControllers(options =>
	{
		var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();
		options.Filters.Add(new AuthorizeFilter(policy));
	});


	services.AddAuthentication(
		options => {
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
					.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			};
		});
	services.AddAuthorization();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
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


app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();


//app.Run("http://localhost:4000");
app.Run();