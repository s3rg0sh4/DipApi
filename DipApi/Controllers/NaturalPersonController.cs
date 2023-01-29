namespace DipApi.Controllers;

using DipApi.Models;
using DipApi.Services;
using DipApi.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[action]")]
[ApiController]
public class NaturalPersonController : ControllerBase
{
	private readonly INaturalPersonService _naturalPersonService;
	private readonly UserManager<User> _userManager;

	public NaturalPersonController(INaturalPersonService naturalPersonService, UserManager<User> userManager) 
	{ 
		_naturalPersonService = naturalPersonService;
		_userManager = userManager;
	}

	[HttpPost]
	public async Task<IActionResult> Create(NaturalPerson naturalPerson)
	{
		if (Request.Cookies.TryGetValue("email", out var email))
		{
			Guid guid = _naturalPersonService.CreateNaturalPerson(naturalPerson);
			var user = await _userManager.FindByEmailAsync(email);
			user.NaturalPersonGuid = guid;

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return Ok(); //мб чёт вернём
			}
		}
		return BadRequest();
	}
}