using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArmWrestling.Applications.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public Team Create(string name, Competition competition)
        {
            Team team = new Team()
            {
                Name = name,
                Competition = competition
            };
            if (_teamRepository.Create(team))
                return team;
            else
                return null;
        }
    }
}
