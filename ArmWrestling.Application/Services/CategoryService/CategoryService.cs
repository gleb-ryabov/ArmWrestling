using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public string CreateCategoryName(Category category)
        {
            string Name = "";

            //Set man or woman
            if (category.Gender == 1 && category.MaxAge == 255)
                Name = "Мужчины";
            else if (category.Gender == 1)
                Name = "Юниоры";
            else if (category.Gender == 0 && category.MaxAge == 255)
                Name = "Женщины";
            else if (category.Gender == 0)
                Name = "Юниорки";

            //Set age and weight
            if (category.MinAge == 0 && category.MaxAge == 255 && category.MaxWeight == 255)
                Name = String.Concat(Name, " Абсолютная");
            else if (category.MaxAge == 255)
                Name = String.Concat(Name, " от ", category.MinAge, " лет до ", category.MaxWeight, " кг");
            else
                Name = String.Concat(Name, " ", category.MinAge, "-", category.MaxAge,
                    " до ", category.MaxWeight, " кг");

            return Name;
        }

    }
}
