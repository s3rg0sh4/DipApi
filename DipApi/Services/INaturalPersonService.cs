namespace DipApi.Services;

using DipApi.Models;

public interface INaturalPersonService
{
	Guid CreateNaturalPerson(NaturalPerson naturalPerson);
}