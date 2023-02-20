using DipApi.Entities;
using DipApi.Models;
using DipApi.Services;
using DipApi.Services.Implementation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DipApi.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	[AllowAnonymous]
	public class DirectumController : ControllerBase
	{
		private readonly IEmailService _emailService;
		private readonly IRateService _rateService;
		private readonly UserManager<User> _userManager;

		public DirectumController(IEmailService emailService, UserManager<User> userManager, IRateService rateService)
		{
			_emailService = emailService;
			_userManager = userManager;
			_rateService = rateService;
		}
		//отдать директуму список ставок

		[HttpGet]
		public async Task<IActionResult> GetRateList(RateRequest rate)
		{
			List<Rate> rates = _rateService.GetRateList(rate.SubdivisionGuid);

			return Ok(rates);
		}



		[HttpPost]
		public async Task<IActionResult> SignOn(RegisterDirectum model)
		{
			var user = new User(model.Email);

			var tryFind = await _userManager.FindByEmailAsync(model.Email);

			if (tryFind != null)
				return BadRequest("User already exist");

			await _userManager.CreateAsync(user);

			try
			{
				string emailBody = $"Ссылка для регистрации\nhttp://localhost:3000/register/{user.Id}";
				_emailService.SendEmailAsync(user.Email, emailBody);
			}
			catch (Exception)
			{
				BadRequest("Email not sent");
			}

			return Ok("Email sent");
		}
	}
}
