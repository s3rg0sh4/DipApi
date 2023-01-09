namespace DipApi.Entities
{
	public class Subdivision
	{
		public int Id { get; set; }
		public Guid GuidSubdivision			{ get; set; }   //Guid подразделения
		public Guid GuidSubdivisionManager	{ get; set; }   //Guid сотрудника - руководителя подразделения
		public string? SubdivisionName		{ get; set; }	//Наименование подразделения
	}
}
