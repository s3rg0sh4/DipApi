using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DipApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize(Roles = "User")]
	public class UserController : ControllerBase
	{
		//TODO: учимся передавать штуки из 1с во фронт
	}
}
