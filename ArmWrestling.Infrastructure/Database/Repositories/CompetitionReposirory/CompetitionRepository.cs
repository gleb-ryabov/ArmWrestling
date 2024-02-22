using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.CompetitionReposirory
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public CompetitionRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(Competition entity)
        {
            _applicationContext.Competitions.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(Competition entity)
        {
            _applicationContext.Competitions.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public Competition Get(int id)
        {
            return _applicationContext.Competitions.Find(id);
        }

        public IEnumerable<Competition> GetAll()
        {
            return _applicationContext.Competitions.ToList();
        }
    }
}
