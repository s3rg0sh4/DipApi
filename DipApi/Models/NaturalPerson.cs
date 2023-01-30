namespace DipApi.Models;

public class NaturalPerson
{
    public Guid GuidNaturalPerson { get; set; } //Guid физлица
    public Guid GuidHiringOrder { get; set; }   //Заявление о приеме на работу
    public string Name { get; set; }   //Имя
    public string MiddleName { get; set; } //Отчество
    public string Surname { get; set; }    //Фамилия
    public string PassportSeries { get; set; } //Серия паспорта
    public string PassportNumber { get; set; } //Номер паспорта
    public string SnilsNumber { get; set; }    //Номер СНИЛС
    public string InnNumber { get; set; }  //Номер ИНН
    public string RegistrationAddress { get; set; }    //Адрес регистрации
    public string Citizenship { get; set; }    //Гражданство
    public string RealAddress { get; set; }    //Адрес фактического проживания
    public string PhoneNumber { get; set; }    //Номер мобильного телефона
    public string Email { get; set; }  //Адрес электронной почты
    public string[]? AdditionalData { get; set; }   //Дополнительные сведения для личного листка учета кадров (тут неуверен, куда это пихать)

    //public NaturalPerson()
    //{
    //    Name = string.Empty;
    //    MiddleName = string.Empty;
    //    Surname = string.Empty;
    //    PassportSeries = string.Empty;
    //    PassportNumber = string.Empty;
    //    RegistrationAddress = string.Empty;
    //    Citizenship = string.Empty;
    //    RealAddress = string.Empty;
    //    PhoneNumber = string.Empty;
    //    Email = string.Empty;
    //    SnilsNumber = string.Empty;
    //    InnNumber = string.Empty;
    //}
}