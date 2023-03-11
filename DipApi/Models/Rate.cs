namespace DipApi.Models;

public class Rate
{
    public Guid GuidCandidate { get; set; }   //Guid кандидата может быть ссылкой
    public string Position { get; set; } = string.Empty; //Название должности
	public bool IsOpen { get; set; } //Флаг свободности
	public double Wage { get; set; } //Доля ставки
}

    //public string Subdivision { get; set; }    //Подразделение вывожу список по этой штуке