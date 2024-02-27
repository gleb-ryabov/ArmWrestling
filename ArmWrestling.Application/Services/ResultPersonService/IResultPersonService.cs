using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.ResultPersonService
{
    public interface IResultPersonService
    {
        ResultPerson Create(CategoryInCompetition categoryInCompetition, Person person, int place);
    }
}