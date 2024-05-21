using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.ResultTeamRepository
{
    public interface IResultTeamRepository : IBaseRepository<ResultTeam>
    {
        bool ExistResult(int competitionId);
        IEnumerable<ResultTeam> GetResulByCompetition(int competitionId);
    }
}
