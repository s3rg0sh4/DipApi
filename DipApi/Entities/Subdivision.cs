namespace DipApi.Entities
{
	public class Subdivision
	{
		Guid GuidSubdivision		{ get; set; }//Guid подразделения
		Guid GuidSubdivisionManager { get; set; }//Guid сотрудника - руководителя подразделения
		string? SubdivisionName		{ get; set; }//Наименование подразделения
	}
}
