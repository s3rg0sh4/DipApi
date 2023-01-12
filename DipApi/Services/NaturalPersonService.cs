using DipApi.Entities;

namespace DipApi.Services
{
	public interface INaturalPersonService
	{
		Guid CreateNaturalPerson(NaturalPerson naturalPerson);
	}
	public class NaturalPersonService : INaturalPersonService
	{
		public Guid CreateNaturalPerson(NaturalPerson naturalPerson)
		{
			return new Guid();
		}
	}
}
