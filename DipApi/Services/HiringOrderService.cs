using DipApi.Entities;
using DipApi.Enums;

namespace DipApi.Services
{
	public interface IHiringOrderService
	{
		HiringOrder CreateHiringOrder(Guid guidHiringApplication);
		byte[] GetPrintingFormHiringOrder(Guid guidPrintingForm);
		bool SendHiringOrderDirectumId(Guid guidHiringOrder, int DirectumTaskId);
		bool SendHiringOrderStatusDirectum(Guid guidDismissalOrder, HiringOrderStatus status = HiringOrderStatus.Approved);
	}

	public class HiringOrderService : IHiringOrderService
	{
		public HiringOrder CreateHiringOrder(Guid guidHiringApplication)
		{
			return new HiringOrder();
		}

		public byte[] GetPrintingFormHiringOrder(Guid guidPrintingForm)
		{
			throw new NotImplementedException();
		}

		public bool SendHiringOrderDirectumId(Guid guidHiringOrder, int DirectumTaskId)
		{
			throw new NotImplementedException();
		}

		public bool SendHiringOrderStatusDirectum(Guid guidDismissalOrder, HiringOrderStatus status = HiringOrderStatus.Approved)
		{
			throw new NotImplementedException();
		}
	}
}
