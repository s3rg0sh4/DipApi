namespace DipApi.Models;

public class Candidate
{
    public Guid GuidCandidate { get; set; }   //Guid кандидата может быть ссылкой (забанить можно)
    public string JobTitle { get; set; }   //Должность
    public string Subdivision { get; set; }    //Подразделение
    public string RateType { get; set; }   //Тип ставки
}