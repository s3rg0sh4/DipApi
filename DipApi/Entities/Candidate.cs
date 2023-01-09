namespace DipApi.Entities
{
	public class Candidate
	{
		Guid GuidCandidate	{ get; set; }//Guid кандидата
		string? JobTitle	{ get; set; }//Должность
		string? Subdivision { get; set; }//Подразделение
		string? TypeRate	{ get; set; }//Тип ставки
	}
}
