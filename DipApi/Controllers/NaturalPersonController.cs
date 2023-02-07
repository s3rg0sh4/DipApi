namespace DipApi.Controllers;

using DipApi.Models;
using DipApi.Services;
using DipApi.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
	//[AllowAnonymous]
	public async Task<IActionResult> Create(CreateModel model)
	{
		User user;
		try
		{
			user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
			{
				throw new Exception();
			}
		}
		catch (Exception)
		{
			return BadRequest("User with such email doesn`t exist");
		}
		Guid guid = _naturalPersonService.CreateNaturalPerson(new NaturalPerson(model));
		user.NaturalPersonGuid = guid;

		var result = await _userManager.UpdateAsync(user);
		if (result.Succeeded)
		{
			return Ok("Natural person created"); //мб чёт вернём
		}
		return BadRequest("Couldn`t create");

	}
}