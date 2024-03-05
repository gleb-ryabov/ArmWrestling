using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.CategoryInCompetitionService
{
    public class CategoryInCompetitionService : ICategoryInCompetitionService
    {
        private readonly ICategoryInCompetitionRepository _categoryInCompetitionRepository;

        public CategoryInCompetitionService(ICategoryInCompetitionRepository categoryInCompetitionRepository)
        {
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
        }
        public CategoryInCompetition Create(Category category, Competition competition)
        {
            CategoryInCompetition categoryInCompetition = new CategoryInCompetition()
            {
                CategoryId = category.Id,
                CompetitionId = competition.Id
            };
            if (_categoryInCompetitionRepository.Create(categoryInCompetition))
                return categoryInCompetition;
            else
                return null;
        }

    }
}
