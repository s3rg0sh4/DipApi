using DipApi.Models;

namespace DipApi.Services
{
	public interface IRateService
	{
		List<Rate> GetRateList(Guid subdivisionGuid);
	}
}
