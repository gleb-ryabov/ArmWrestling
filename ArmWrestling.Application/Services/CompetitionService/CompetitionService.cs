using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.CompetitionService
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;

        public CompetitionService(ICompetitionRepository competitionRepository)
        {
            _competitionRepository = competitionRepository;
        }

        public Competition Create(string typeJudging, string typeCompetition, 
            byte countTable, char firstArm, int weightTolerance)
        {
            Competition competition = new Competition()
            {
                Created = DateTime.Now,
                TypeJudging = typeJudging,
                TypeCompetition = typeCompetition,
                CountTable = countTable,
                FirstArm = firstArm,
                WeightTolerance = weightTolerance
            };
            if (_competitionRepository.Create(competition))
                return competition;
            else
                return null;
        }
    }
}
