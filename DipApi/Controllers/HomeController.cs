using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DipApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Authorize(Roles = "User")]
	public class HomeController : ControllerBase
	{

	}
}
