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
        int GetDuelByPerson(char arm, Person person);
        int GetCountLossesByPerson(char arm, Person person);
    }
}
