using DipApi.Enums;
using DipApi.Models;

namespace DipApi.Services.Impl
{
	public class StatusService : IStatusService
	{
		public HiringOrderStatus GetOrderStatus(Guid orderGuid)
		{
			//get hiring order by guid
			var order = new HiringOrder();


			return order.OrderStatus;
		}
	}
}
