namespace DipApi.Entities
{
	public class HiringOrder
	{
		bool? IsError;                           //Если ошибка
		string? ErrorString;                     //Сообщении об ошибке
		Guid? GuidHiringOrder = null;        //Guid приказа
		Guid? GuidPrintingForm = null;          //Guid печатной формы
		int? IdDirectim = null;                 //Id задачи согласования приказа в директуме
		string? OrderNumber = null;              //Номер приказа
		string? DateOrder = null;                //Дата приказа
		HiringOrderStatus? OrderStatus = HiringOrderStatus.Undefined; //Статус приказа
	}
}
