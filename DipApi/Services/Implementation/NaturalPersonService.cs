namespace DipApi.Services.Implementation;

using DipApi.DB;
using DipApi.Models;

public class NaturalPersonService : INaturalPersonService
{
	private readonly UserContext _context;

    public NaturalPersonService(UserContext context)
	{
		_context = context;
	}

	public Guid CreateNaturalPerson(NaturalPerson naturalPerson)
    {




        //вот тут мы что-то делаем в 1с и директум
        return Guid.NewGuid();
    }
}