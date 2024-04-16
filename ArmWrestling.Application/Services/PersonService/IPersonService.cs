using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.PersonService
{
    public interface IPersonService
    {
        Person Create(string surname, string name, string middleName, byte gender, DateOnly birthDate,
            float weight, CategoryInCompetition category, Team team);
        List<Person> GetPersonsInGroupA(CategoryInCompetition category, char arm);
        List<Person> GetPersonsInGroupB(CategoryInCompetition category, char arm);
        List<Person> GetNonDroppedOutPersons(CategoryInCompetition category, char arm);
        List<List<Person>> CreateDraw(CategoryInCompetition category, char arm);
        byte GetTypeDuel(CategoryInCompetition category, char arm);
    }
}