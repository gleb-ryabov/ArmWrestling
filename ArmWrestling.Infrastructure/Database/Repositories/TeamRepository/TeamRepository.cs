using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.TeamRepository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationContext _applicationContext;
        public TeamRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(Team entity)
        {
            _applicationContext.Teams.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(Team entity)
        {
            _applicationContext.Teams.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public Team Get(int id)
        {
            return _applicationContext.Teams.Find(id);
        }

        public IEnumerable<Team> GetAll()
        {
            return _applicationContext.Teams.ToList();
        }

        public IEnumerable<Team> GetByCompetition(int competitionId)
        {
            return _applicationContext.Teams
                .Where(t => t.CompetitionId == competitionId)
                .OrderBy(t => t.Name)
                .ToList();
        }

        //Function for add score to team
        public bool AddScore(Team team, int score)
        {
            Team modifiedTeam = Get(team.Id);
            modifiedTeam.Score += score;
            return _applicationContext.SaveChanges() > 0;
        }

        //Get teams (order by score)
        public IEnumerable<Team> GetOrderByScore(int competitionId)
        {
            return _applicationContext.Teams
                .Where(t => t.CompetitionId == competitionId)
                .OrderByDescending(t => t.Score)
                .ToList();
        }
    }
}
