using DipApi.Models;
using DipApi.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApi.Entities;

namespace DipApi.Controllers
{
    [Authorize]
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class NaturalPersonController : ControllerBase
	{
		private readonly INaturalPersonService _naturalPersonService;
		private readonly UserManager<User> _userManager;

		private NaturalPerson person = new NaturalPerson();

		public NaturalPersonController(INaturalPersonService naturalPersonService, UserManager<User> userManager) 
		{ 
			_naturalPersonService = naturalPersonService;
			_userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> CreateNaturalPerson(string email)
		{
			Guid guid = _naturalPersonService.CreateNaturalPerson(person);
			var user = await _userManager.FindByEmailAsync(email);
			user.NaturalPersonGuid = guid;

			return Ok(); //мб чёт вернём
		}

	}
}
