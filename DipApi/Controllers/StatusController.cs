namespace DipApi.Controllers;

using DipApi.Entities;
using DipApi.Models;
using DipApi.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


[Route("api/[action]")]
[ApiController]
public class StatusController : ControllerBase
{

	private readonly IStatusService _statusService;
	private readonly UserManager<User> _userManager;

	public StatusController(IStatusService statusService, UserManager<User> userManager)
	{
		_statusService = statusService;
		_userManager = userManager;
	}

	//[HttpGet]
	//public async Task<IActionResult> Status(string email)
	//{

	//	var status = new Status();
	//	status.applicationStatus = _statusService.GetApplicationStatus(new Guid());
	//	status.orderStatus = _statusService.GetOrderStatus(new Guid());

	//	return Ok();
	//}

	[HttpGet]
	public async Task<IActionResult> HiringStatus(string email)
	{
		//надо подумать, какие данные видно юзеру

		return Ok();
	}
}
