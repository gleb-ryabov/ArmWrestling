using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.ResultPersonRepository
{
    public class ResultPersonRepository : IResultPersonRepository
    {
        private readonly ApplicationContext _applicationContext;
        public ResultPersonRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(ResultPerson entity)
        {
            _applicationContext.ResultPersons.Add(entity);
            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(ResultPerson entity)
        {
            _applicationContext.ResultPersons.Remove(entity);
            return _applicationContext.SaveChanges() > 0;
        }

        public ResultPerson Get(int id)
        {
            return _applicationContext.ResultPersons.Find(id);
        }

        public IEnumerable<ResultPerson> GetAll()
        {
            return _applicationContext.ResultPersons.ToList();
        }
    }
}
