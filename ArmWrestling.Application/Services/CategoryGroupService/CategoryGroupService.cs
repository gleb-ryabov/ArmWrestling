using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArmWrestling.Applications.Services.CategoryGroupService
{
    public class CategoryGroupService : ICategoryGroupService
    {
        private readonly ICategoryGroupRepository _categoryGroupRepository;

        public CategoryGroupService(ICategoryGroupRepository categoryGroupRepository) 
        {
            _categoryGroupRepository = categoryGroupRepository;
        }

        public CategoryGroup Create(string name)
        {
            CategoryGroup categoryGroup = new CategoryGroup
            {
                Name = name
            };
            if (_categoryGroupRepository.Create(categoryGroup))
                return categoryGroup;
            else 
                return null;
        }
    }
}
