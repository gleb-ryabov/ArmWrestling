using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryGroupRepository;
using ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TableRepository;
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
        private readonly IDuelRepository _duelRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IPersonRepository _personRepository;

        public CategoryInCompetitionService(
            ICategoryInCompetitionRepository categoryInCompetitionRepository,
            IDuelRepository duelRepository,
            ITableRepository tableRepository,
            IPersonRepository personRepository)
        {
            _categoryInCompetitionRepository = categoryInCompetitionRepository;
            _duelRepository = duelRepository;
            _tableRepository = tableRepository;
            _personRepository = personRepository;
        }

        public CategoryInCompetition Create(Category category, Competition competition)
        {
            CategoryInCompetition categoryInCompetition = new CategoryInCompetition()
            {
                CategoryId = category.Id,
                CompetitionId = competition.Id,
                Complited = 0
            };
            if (_categoryInCompetitionRepository.Create(categoryInCompetition))
                return categoryInCompetition;
            else
                return null;
        }

        //Function for getting CategoryInCompetition, who have not participated in the competition yet
        public List<CategoryInCompetition> GetNotUsedCategories(Competition competition)
        {
            //List for all categories in competition
            List<CategoryInCompetition> allCategories = 
                _categoryInCompetitionRepository.GetByCompetition(competition).ToList();

            //List for categories in duel
            List<CategoryInCompetition> usedCategoriesInDuels = 
                _duelRepository.GetUsedCategories(competition).ToList();

            //List for categories in table
            List<CategoryInCompetition> usedCategoriesInTable = 
                _tableRepository.GetUsedCategories(competition).ToList();

            //List for remove categories
            List<CategoryInCompetition> CategoriesForRemove = new List<CategoryInCompetition>();

            //Add categories in list for remove categories, who haven't persons
            foreach(var category in allCategories)
            {
                int countPerson = _personRepository.GetPersonCountByCategory(category);

                if(countPerson == 0)
                    CategoriesForRemove.Add(category);
            }

            //Add categories in list for remove categories, who use in Duels
            FindSimilarCategories(usedCategoriesInDuels);

            //Add categories in list for remove categories, who use in Table
            FindSimilarCategories(usedCategoriesInTable);

            //Function for find similar categories in the main list and the other list
            //      and add result in Categoris for remove (list)
            void FindSimilarCategories(List<CategoryInCompetition> usedCategories)
            {
                foreach (var category in usedCategories)
                {
                    foreach (var allCategory in allCategories)
                    {
                        if (allCategory.Id == category.Id && !CategoriesForRemove.Contains(allCategory))
                            CategoriesForRemove.Add(allCategory);
                    }
                }
            }

            //Delete used categories in main list
            foreach(var category in CategoriesForRemove)
            {
                allCategories.Remove(category);
            }

            return allCategories;
        }
    }
}
