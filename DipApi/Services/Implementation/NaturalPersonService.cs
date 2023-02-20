namespace DipApi.Services.Implementation;

using DipApi.Models;

public class NaturalPersonService : INaturalPersonService
{
    public Guid CreateNaturalPerson(NaturalPerson naturalPerson)
    {
        //вот тут мы что-то делаем в 1с и директум
        return Guid.NewGuid();
    }
}