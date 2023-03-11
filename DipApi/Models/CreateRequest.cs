namespace DipApi.Models
{
	public class CreateRequest
	{
		public string UserEmail { get; set; }
		public string NaturalPerson { get; set; }
		public List<IFormFile> Files { get; set; }
	}
}
