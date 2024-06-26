﻿using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.DuelRepository
{
    public interface IDuelRepository : IBaseRepository<Duel>
    {
        int GetCountDuelByPerson(char arm, Person person);
        int GetCountLossesByPerson(char arm, Person person);
        int GetCountLossesByPersonInTour(char arm, int tour, Person person);
        int GetCountWinByPerson(char arm, Person person);
        int GetLastNumberTour(char arm, CategoryInCompetition category);
        char GetLastArmInCategory(CategoryInCompetition category);
        bool CheckPersonForFreeCircle(char arm, CategoryInCompetition category, Person person, int tourNumber);
        int GetCountDuelsBetweenPersons(char arm, Person person, Person opponent);
        bool CheckDefeatInLastRound(char arm, int tour, Person person);
        IEnumerable<CategoryInCompetition> GetUsedCategories(Competition competition);
        bool CheckExstenceDuels(CategoryInCompetition categoryInCompetition, char arm);
        bool SetWinnerAndLooser(Duel duel, Person winner, Person looser);
        IEnumerable<Person> GetLoosersInTour(CategoryInCompetition categoryInCompetition, char arm, int tourNumber);
        int GetWinnerIdInTour(CategoryInCompetition categoryInCompetition, char arm, int tourNumber);
        bool CheckOpponentsInLastRound(CategoryInCompetition categoryInCompetition, char arm,
            Person opponent1, Person opponent2);
    }
}
