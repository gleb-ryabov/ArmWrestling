using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.PersonService
{
    public interface IPersonService
    {
        Person Create(string surname, string name, string middleName, byte gender, DateOnly birthDate,
            float weight, CategoryInCompetition category, Team team = null);
    }
}