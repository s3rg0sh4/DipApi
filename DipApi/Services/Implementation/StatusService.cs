namespace DipApi.Services.Implementation;

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

	public HiringApplicationStatus GetApplicationStatus(Guid applicationGuid)
	{
		//get hiring application by guid
		var application = new HiringApplication();

		return application.ApplicationStatus;
	}
}