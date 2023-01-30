using DipApi.Enums;

namespace DipApi.Models;

public class HiringApplication
{

    public bool? IsError { get; set; }  //Если данные не прошли проверку
    public string? ErrorString { get; set; }    //Сообщение об ошибке
    public DateTime? DateOfApply { get; set; }  //Дата приема
    public bool? Physical { get; set; } //true если трудовая физическая, false - электронная
    public int? SigningId { get; set; } //Id подписания
    public HiringApplicationSigningStatus? Status { get; set; } = HiringApplicationSigningStatus.Undefined; //Статус согласования
    public Guid? GuidPrintingForm { get; set; } = null; //Guid печатной формы
    public int? IdDirectum { get; set; } = null;    //Id задачи согласования документа в директуме
    public Guid? GuidHiringApplication { get; set; } = null;    //Guid заявления о приеме
}
