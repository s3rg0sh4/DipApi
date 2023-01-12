namespace DipApi.Entities
{
	public class Candidate
	{
		public int Id { get; set; }
		public string? JobTitle		{ get; set; }   //Должность
		public string? Subdivision	{ get; set; }	//Подразделение
		public string? RateType		{ get; set; }	//Тип ставки
		public Guid? GuidCandidate	{ get; set; }   //Guid кандидата
	}
}
