using DipApi.Models;

namespace DipApi.Services.Impl
{

    public class NaturalPersonService : INaturalPersonService
    {
        public Guid CreateNaturalPerson(NaturalPerson naturalPerson)
        {
            //вот тут мы что-то делаем в 1с и директум
            return Guid.NewGuid();
        }
    }
}
