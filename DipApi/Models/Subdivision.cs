namespace DipApi.Models
{
    public class Subdivision //На основе данных этой штуки генерится ссылка
    {
        public Guid GuidSubdivision { get; set; }   //Guid подразделения
        public string SubdivisionName { get; set; }    //Наименование подразделения
        public Guid GuidSubdivisionManager { get; set; }   //Guid сотрудника - руководителя подразделения
    }
}
