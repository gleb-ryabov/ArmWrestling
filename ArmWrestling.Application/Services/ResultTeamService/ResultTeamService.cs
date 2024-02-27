using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.ResultTeamRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.ResultTeamService
{
    public class ResultTeamService : IResultTeamService
    {
        private readonly IResultTeamRepository _resultTeamRepository;

        public ResultTeamService(IResultTeamRepository resultTeamRepository)
        {
            _resultTeamRepository = resultTeamRepository;
        }
        public ResultTeam Create(Competition competition, Team team, int place)
        {
            ResultTeam resultTeam = new ResultTeam()
            {
                Competition = competition,
                Team = team,
                Place = place
            };
            if (_resultTeamRepository.Create(resultTeam))
                return resultTeam;
            else
                return null;
        }
    }
}
