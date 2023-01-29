using DipApi.Enums;

namespace DipApi.Services
{
    public interface IStatusService
    {
        HiringOrderStatus GetOrderStatus(Guid orderGuid);
    }
}
