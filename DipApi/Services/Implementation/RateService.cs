using DipApi.Models;

namespace DipApi.Services.Implementation
{
	public class RateService : IRateService
	{
		public List<Rate> GetRateList(Guid subdivisionGuid)
		{
			var rateList = new List<Rate>();
			rateList.Add(new Rate());
			rateList.Add(new Rate());
			rateList.Add(new Rate());
			return rateList;
		}
	}
}
