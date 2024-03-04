using ArmWrestling.Domain.Database;
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

        public DuelService(IDuelRepository duelRepository)
        {
            _duelRepository = duelRepository;
        }

        public Duel Create(CategoryInCompetition categoryInCompetition, Person winnerPerson,
            Person looserPerson, char arm, int tourNumber, byte typeDuel = 0, char group = ' ')
        {
            Duel duel = new Duel()
            {
                CategoryInCompetition = categoryInCompetition,
                Winner = winnerPerson,
                Looser = looserPerson,
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


    }
}
