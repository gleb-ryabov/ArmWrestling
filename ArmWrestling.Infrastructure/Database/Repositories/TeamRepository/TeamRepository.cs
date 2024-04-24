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
    }
}
