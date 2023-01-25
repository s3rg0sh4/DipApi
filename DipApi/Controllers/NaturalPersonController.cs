namespace DipApi.Controllers;

using DipApi.Models;
using DipApi.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using DipApi.Entities;

[Authorize]
[ApiController]
[Route("api/[action]")]
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

	[HttpPost(Name = "create")]
	public async Task<IActionResult> Create()
	{
		if (Request.Cookies.TryGetValue("email", out var email))
		{
			Guid guid = _naturalPersonService.CreateNaturalPerson(person);
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