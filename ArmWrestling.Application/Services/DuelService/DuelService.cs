using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.DuelService
{
    public class DuelService : IDuelService
    {
        private readonly IDuelRepository _duelRepository;
        private readonly ICompetitionRepository _competitionRepository;

        public DuelService(IDuelRepository duelRepository,
            ICompetitionRepository competitionRepository)
        {
            _duelRepository = duelRepository;
            _competitionRepository = competitionRepository;
        }

        public Duel Create(CategoryInCompetition categoryInCompetition,
            char arm, int tourNumber, byte typeDuel = 0, char group = ' ')
        {
            Duel duel = new Duel()
            {
                CategoryInCompetitionId = categoryInCompetition.Id,
                Arm = arm,
                TourNumber = tourNumber,
                TypeDuel = typeDuel,
                Group = group
            };
            if (_duelRepository.Create(duel))
                return duel;
            else
                return null;
        }

        //Function fro Calculate the group (А or Б) for duel by persons
        public char CalculateGroup(char arm, Person firstPerson, Person secondPerson)
        {
            int countLossesFirstPerson = _duelRepository.GetCountLossesByPerson(arm, firstPerson);
            int countLossesSecondPerson = _duelRepository.GetCountLossesByPerson(arm, secondPerson);

            if (countLossesFirstPerson == 0 && countLossesSecondPerson == 0)
                return 'А';

            return 'Б';
        }

        //Function for get last arm in duels in category or get default arm
        public char GetLastArmInCategory(CategoryInCompetition category)
        {
            char arm = _duelRepository.GetLastArmInCategory(category);
            if (arm == ' ')
            {
                category.Competition = _competitionRepository.Get(category.CompetitionId);
                arm = category.Competition.FirstArm;
            }

            return arm;
        }
    }
}
