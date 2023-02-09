using DipApi.Enums;

namespace DipApi.Models;

public class Status
{
	public HiringApplicationStatus applicationStatus;
	public HiringOrderStatus orderStatus;

	public Status(HiringApplicationStatus applicationStatus, HiringOrderStatus orderStatus)
	{
		this.applicationStatus = applicationStatus;
		this.orderStatus = orderStatus;
	}
	public Status() { }
}