using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.DuelRepository
{
    public interface IDuelRepository : IBaseRepository<Duel>
    {
        int GetCountDuelByPerson(char arm, Person person);
        int GetCountLossesByPerson(char arm, Person person);
        int GetCountWinByPerson(char arm, Person person);
        int GetLastNumberTour(char arm, CategoryInCompetition category);
        bool CheckPersonForFreeCircle(char arm, CategoryInCompetition category, Person person, int tourNumber);
        int GetCountDuelsBetweenPersons(char arm, Person person, Person opponent);
        bool CheckDefeatInLastRound(char arm, int tour, Person person);
    }
}
