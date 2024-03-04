using ArmWrestling.Domain.Database;
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

        //function for check person for free circle (did he participate in the last round)
        //return true if Person wasn't in free circle
        public bool CheckPersonForFreeCircle(char arm, CategoryInCompetition category, Person person, int tourNumber)
        {
            return _applicationContext.Duels
                .Where(d => d.Arm == arm)
                .Where(d => d.CategoryInCompetition == category)
                .Where(d => d.TourNumber == tourNumber)
                .Where(d => d.Winner == person || d.Looser == person)
                .Any();
        }

        public int GetCountDuelsBetweenPersons(char arm, Person person, Person opponent)
        {
            int countDuel = _applicationContext.Duels
                .Where(d => d.CategoryInCompetition == person.CategoryInCompetition)
                .Where(d => d.Arm == arm)
                .Where(d => d.Winner == person && d.Looser == opponent ||
                            d.Winner == opponent && d.Looser == person)
                .Count();

            return countDuel;
        }

        //function for check defeat in last round
        //return true if Person was Loser in lat round
        public bool CheckDefeatInLastRound(char arm, int tour, Person person)
        {
            bool wasLoser = _applicationContext.Duels
                .Where(d => d.CategoryInCompetition == person.CategoryInCompetition)
                .Where(d => d.Arm == arm)
                .Where(d => d.TourNumber == tour-1)
                .Where(d => d.Looser == person)
                .Any();

            return wasLoser;
        }
    }
}
