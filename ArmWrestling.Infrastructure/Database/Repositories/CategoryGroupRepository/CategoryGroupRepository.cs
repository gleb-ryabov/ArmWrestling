using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository
{
    public class CategoryGroupRepository : ICategoryGroupRepository
    {
        private readonly ApplicationContext _applicationContext;
        public CategoryGroupRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(CategoryGroup entity)
        {
            _applicationContext.CategoryGroups.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(CategoryGroup entity)
        {
            _applicationContext.CategoryGroups.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public CategoryGroup Get(int id)
        {
            return _applicationContext.CategoryGroups.Find(id);
        }

        public IEnumerable<CategoryGroup> GetAll()
        {
            return _applicationContext.CategoryGroups.ToList();
        }
    }
}
