using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ArmWrestling.Applications.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public Category Create(byte gender, byte minAge, byte maxAge, byte maxWeight, CategoryGroup categoryGroup)
        {
            Category category = new Category
            {
                Gender = gender,
                MinAge = minAge,
                MaxAge = maxAge,
                MaxWeight = maxWeight,
                CategoryGroup = categoryGroup
            };
            if (_categoryRepository.Create(category))
                return category;
            else
                return null;
        }

    }
}
