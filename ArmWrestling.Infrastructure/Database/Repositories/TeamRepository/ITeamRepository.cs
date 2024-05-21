using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.TeamRepository
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        IEnumerable<Team> GetByCompetition(int competitionId);
        bool AddScore(Team team, int score);
        IEnumerable<Team> GetOrderByScore(int competitionId);
    }
}
