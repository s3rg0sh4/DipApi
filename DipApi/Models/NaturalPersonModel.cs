namespace DipApi.Models
{
	public class NaturalPersonModel
	{
		public string Email { get; set; } 
		public string Surname { get; set; }    //Фамилия
		public string FirstName { get; set; }   //Имя
		public string MiddleName { get; set; } //Отчество
		public string PassportSeries { get; set; } //Серия паспорта
		public string PassportNumber { get; set; } //Номер паспорта
		public string SnilsNumber { get; set; }    //Номер СНИЛС
		public string InnNumber { get; set; }  //Номер ИНН
		public string RegistrationAddress { get; set; }    //Адрес регистрации
		public string Citizenship { get; set; }    //Гражданство
		public string RealAddress { get; set; }    //Адрес фактического проживания
		public string PhoneNumber { get; set; }    //Номер мобильного телефона
	}
}
