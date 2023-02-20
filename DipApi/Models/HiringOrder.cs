using DipApi.Enums;

namespace DipApi.Models;

public class HiringOrder
{
    public Guid? GuidHiringOrder { get; set; } = null;  //Guid приказа
    public Guid? GuidPrintingForm { get; set; } = null; //Guid печатной формы
    public int? IdDirectim { get; set; } = null;    //Id задачи согласования приказа в директуме
    public bool IsError { get; set; } = false;   //Есть ли ошибка
    public string OrderNumber { get; set; } = string.Empty;    //Номер приказа
    public string DateOrder { get; set; } = string.Empty;  //Дата приказа
    public string? ErrorString { get; set; }    //Сообщении об ошибке
    public HiringOrderStatus OrderStatus { get; set; } = HiringOrderStatus.Undefined;  //Статус приказа
}
