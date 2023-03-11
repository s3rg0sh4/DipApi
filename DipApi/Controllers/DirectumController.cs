using System.Net.Http.Headers;

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
		private readonly IFileService _fileService;

		public DirectumController(IEmailService emailService, UserManager<User> userManager, IRateService rateService, IFileService fileService)
		{
			_emailService = emailService;
			_userManager = userManager;
			_rateService = rateService;
			_fileService = fileService;
		}
		//отдать директуму список ставок

		[HttpGet]
		public async Task<IActionResult> GetRateList(RateRequest rate)
		{
			List<Rate> rates = _rateService.GetRateList(rate.SubdivisionGuid);

			return Ok(rates);
		}

		[HttpGet]
		public async Task<IActionResult> GetUserFiles(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return BadRequest($"User with email {email} doesn`t exist");
			}

			var files = await _fileService.GetUserFiles(user);

			return Ok(files);
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
