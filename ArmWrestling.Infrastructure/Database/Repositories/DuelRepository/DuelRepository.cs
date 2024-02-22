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

        public int GetDuelByPerson(char arm, Person person)
        {
            return _applicationContext.Duels
                .Where(d => (d.Looser == person || d.Winner == person) && d.Arm == arm)
                .Count();
        }
    }
}
