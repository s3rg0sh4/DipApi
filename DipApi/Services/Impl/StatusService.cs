namespace DipApi.Services.Impl;

using DipApi.Enums;
using DipApi.Models;

public class StatusService : IStatusService
{
	public HiringOrderStatus GetOrderStatus(Guid orderGuid)
	{
		//get hiring order by guid
		var order = new HiringOrder();


		return order.OrderStatus;
	}
}