using ArmWrestling.Domain.Database;

namespace ArmWrestling.Applications.Services.DuelService
{
    public interface IDuelService
    {
        Duel Create(CategoryInCompetition categoryInCompetition, 
            char arm, int tourNumber, byte typeDuel, char group);
        public char CalculateGroup(char arm, Person firstPerson, Person secondPerson);
        char GetLastArmInCategory(CategoryInCompetition category);
    }
}