using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _applicationContext;
        public CategoryRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(Category entity)
        {
            _applicationContext.Categories.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(Category entity)
        {
            _applicationContext.Categories.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public Category Get(int id)
        {
            return _applicationContext.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _applicationContext.Categories.ToList();
        }


    }
}
