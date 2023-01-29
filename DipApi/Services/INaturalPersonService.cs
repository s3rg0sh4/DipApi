using DipApi.Models;

namespace DipApi.Services
{
	public interface INaturalPersonService
	{
		Guid CreateNaturalPerson(NaturalPerson naturalPerson);
	}
}
