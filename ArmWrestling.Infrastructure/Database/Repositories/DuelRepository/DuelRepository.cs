using ArmWrestling.Domain.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.DuelRepository
{
    public class DuelRepository : IDuelRepository
    {
        private readonly ApplicationContext _applicationContext;
        public DuelRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(Duel entity)
        {
            _applicationContext.Duels.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(Duel entity)
        {
            _applicationContext.Duels.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public Duel Get(int id)
        {
            return _applicationContext.Duels.Find(id);
        }

        public IEnumerable<Duel> GetAll()
        {
            return _applicationContext.Duels.ToList();
        }

        public int GetCountLossesByPerson(char arm, Person person)
        {
            return _applicationContext.Duels
                .Where(d=> d.Looser == person && d.Arm == arm)
                .Count();
        }
        public int GetCountLossesByPersonInTour(char arm, int tour, Person person)
        {
            return _applicationContext.Duels
                .Where(d => d.Looser == person && d.Arm == arm && d.TourNumber <= tour)
                .Count();
        }
        public int GetCountWinByPerson(char arm, Person person)
        {
            return _applicationContext.Duels
                .Where(d => d.Winner == person && d.Arm == arm)
                .Count();
        }

        public int GetCountDuelByPerson(char arm, Person person)
        {
            return _applicationContext.Duels
                .Where(d => (d.Looser == person || d.Winner == person) && d.Arm == arm)
                .Count();
        }

        public int GetLastNumberTour(char arm, CategoryInCompetition category)
        {
            bool recordsExist = _applicationContext.Duels
                .Any(d => d.CategoryInCompetition == category && d.Arm == arm);

            if (recordsExist)
            {
                return _applicationContext.Duels
                    .Where(d => d.CategoryInCompetition == category && d.Arm == arm)
                    .Max(d => d.TourNumber);
            }
            else
                return 0;
        }

        public char GetLastArmInCategory(CategoryInCompetition category)
        {
            bool recordsExist = _applicationContext.Duels
                .Any(d => d.CategoryInCompetition == category);

            if (recordsExist)
            {
                return _applicationContext.Duels
                .Where(d => d.CategoryInCompetition == category)
                .OrderBy(d => d.Id)
                .Select(d => d.Arm)
                .Last();
            }
            else
                return ' ';
        }

        //Function for check person for free circle (did he participate in the last round)
        //  return true if Person was in free circle
        public bool CheckPersonForFreeCircle(char arm, CategoryInCompetition category, Person person, int tourNumber)
        {
            return _applicationContext.Duels
                .Where(d => d.Arm == arm)
                .Where(d => d.CategoryInCompetition == category)
                .Where(d => d.TourNumber == tourNumber)
                //.Where(d => d.Winner == person || d.Looser == person)
                .Where(d => d.WinnerId == person.Id && d.Looser == null)
                .Any();
        }

        public int GetCountDuelsBetweenPersons(char arm, Person person, Person opponent)
        {
            int countDuel = _applicationContext.Duels
                .Where(d => d.CategoryInCompetitionId == person.CategoryInCompetitionId)
                .Where(d => d.Arm == arm)
                .Where(d => d.WinnerId == person.Id && d.LooserId == opponent.Id ||
                            d.WinnerId == opponent.Id && d.LooserId == person.Id)
                .Count();

            return countDuel;
        }

        //Function for check defeat in last round
        //  return true if Person was Loser in last round
        public bool CheckDefeatInLastRound(char arm, int tour, Person person)
        {
            bool wasLoser = _applicationContext.Duels
                .Where(d => d.CategoryInCompetitionId == person.CategoryInCompetitionId)
                .Where(d => d.Arm == arm)
                .Where(d => d.TourNumber == tour-1)
                .Where(d => d.LooserId == person.Id)
                .Any();

            return wasLoser;
        }

        //Function for getting CategoryInCompetition in which there have already been duels
        public IEnumerable<CategoryInCompetition> GetUsedCategories(Competition competition)
        {
            return _applicationContext.Duels
                .Select(d => d.CategoryInCompetition)
                .Where(d => d.CompetitionId == competition.Id)
                .Distinct()
                .ToList();
        }

        //Function for check existence of duels by CategoryInCompetition and arm
        public bool CheckExstenceDuels(CategoryInCompetition categoryInCompetition, char arm)
        {
            return _applicationContext.Duels
                .Where(d => d.CategoryInCompetitionId ==  categoryInCompetition.Id)
                .Where(d => d.Arm == arm)
                .Any();
        }

        //Function for set winner and looser
        public bool SetWinnerAndLooser(Duel duel, Person winner, Person looser)
        {
            Duel findingDuel = Get(duel.Id);

            findingDuel.Winner = winner;
            findingDuel.Looser = looser;

            if (_applicationContext.SaveChanges() > 0)
                return true;

            return false;
        }

        //Function for get loosers in tour
        public IEnumerable<Person> GetLoosersInTour(CategoryInCompetition categoryInCompetition,
            char arm, int tourNumber)
        {
            return _applicationContext.Duels
                .Where(d => d.CategoryInCompetition.Id == categoryInCompetition.Id)
                .Where(d => d.Arm == arm)
                .Where(d => d.TourNumber == tourNumber)
                .Select(d => d.Looser)
                .ToList();
        }

        //Function for getting id person who winner in tour
        public int GetWinnerIdInTour(CategoryInCompetition categoryInCompetition, char arm, int tourNumber)
        {
            return _applicationContext.Duels
                .Where(d => d.CategoryInCompetition.Id == categoryInCompetition.Id)
                .Where(d => d.Arm == arm)
                .Where(d => d.TourNumber == tourNumber)
                .Select(d => d.WinnerId.GetValueOrDefault())
                .FirstOrDefault();
        }

        //Function for check to did the opponents meet in the last round
        public bool CheckOpponentsInLastRound(CategoryInCompetition categoryInCompetition, char arm,
            Person opponent1, Person opponent2)
        {
            return _applicationContext.Duels
                .Where(d => d.CategoryInCompetitionId == categoryInCompetition.Id)
                .Where(d => d.Arm == arm)
                .Where(d => (d.Winner == opponent1 && d.Looser == opponent2) || 
                            (d.Winner == opponent2 && d.Looser == opponent1))
                .Any();
        }
    }
}
