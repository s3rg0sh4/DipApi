using DipApi.Enums;

namespace DipApi.Entities
{
	public class HiringOrder
	{
		public int Id { get; set; }
		public bool? IsError					{ get; set; }	//Есть ли ошибка
		public string? ErrorString				{ get; set; }	//Сообщении об ошибке
		public Guid? GuidHiringOrder			{ get; set; } = null;	//Guid приказа
		public Guid? GuidPrintingForm			{ get; set; } = null;	//Guid печатной формы
		public int? IdDirectim					{ get; set; } = null;	//Id задачи согласования приказа в директуме
		public string? OrderNumber				{ get; set; } = null;	//Номер приказа
		public string? DateOrder				{ get; set; } = null;	//Дата приказа
		public HiringOrderStatus? OrderStatus	{ get; set; } = HiringOrderStatus.Undefined;	//Статус приказа
	}
}
