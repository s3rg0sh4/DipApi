namespace DipApi.Entities
{
	public class HiringApplication
	{
		bool? IsError;                           //Если данные не прошли проверку
		string? ErrorString;                     //Сообщение об ошибке
		Guid? GuidHiringApplication = null;     //Guid заявления о приеме
		Guid? GuidPrintingForm = null;          //Guid печатной формы
		DateTime? DateOfApply;                   //Дата приема
		bool? Physical;                          //true если трудовая физическая, false - электронная
		int? IdDirectum = null;                 //Id задачи согласования документа в директуме
		HiringApplicationSigningStatus? Status = HiringApplicationSigningStatus.Undefined; //Статус согласования
		int? signingId;                         //Id подписания
	}
}
