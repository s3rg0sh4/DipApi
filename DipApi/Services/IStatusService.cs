﻿namespace DipApi.Services;

using DipApi.Enums;

public interface IStatusService
{
    HiringOrderStatus GetOrderStatus(Guid orderGuid);
}
