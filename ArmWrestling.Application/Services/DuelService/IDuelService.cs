using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.DuelService
{
    public interface IDuelService
    {
        Duel Create(CategoryInCompetition categoryInCompetition, Person winnerPerson,
            Person looserPerson, char arm, int tourNumber, byte typeDuel, char group);
    }
}