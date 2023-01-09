namespace DipApi.Entities
{
	public class Candidate
	{
		public int Id { get; set; }
		public Guid GuidCandidate	{ get; set; }   //Guid кандидата
		public string? JobTitle		{ get; set; }   //Должность
		public string? Subdivision	{ get; set; }	//Подразделение
		public string? TypeRate		{ get; set; }	//Тип ставки
	}
}
