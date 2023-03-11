namespace DipApi.Controllers;

using DipApi.Models;
using DipApi.Services;
using DipApi.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DipApi.DB;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

[Route("api/[action]")]
[ApiController]
public class NaturalPersonController : ControllerBase
{
	private readonly INaturalPersonService _naturalPersonService;
	private readonly UserManager<User> _userManager;
	private readonly IFileService _fileService;

	public NaturalPersonController(INaturalPersonService naturalPersonService, UserManager<User> userManager, IFileService fileService)
	{
		_naturalPersonService = naturalPersonService;
		_userManager = userManager;
		_fileService = fileService;
	}

	[HttpPost]
	[AllowAnonymous]
	[RequestSizeLimit(10_000_000)]
	public async Task<IActionResult> Create([FromForm]CreateRequest model)
	{
		var user = await _userManager.FindByEmailAsync(model.UserEmail);
		if (user == null)
		{
			return BadRequest($"User with email {model.UserEmail} doesn`t exist");
		}
		try
		{
			_fileService.SaveFiles(model.Files, user.Id);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}

		NaturalPersonModel person = JsonConvert.DeserializeObject<NaturalPersonModel>(model.NaturalPerson);

		Guid guid = _naturalPersonService.CreateNaturalPerson(new NaturalPerson(person));
		user.NaturalPersonGuid = guid;

		var result = await _userManager.UpdateAsync(user);
		if (result.Succeeded)
		{
			return Ok(); //мб чёт вернём
		}
		return BadRequest("Couldn`t create");
	}
}