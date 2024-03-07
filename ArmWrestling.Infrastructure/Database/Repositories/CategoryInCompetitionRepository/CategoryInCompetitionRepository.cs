using ArmWrestling.Domain.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository
{
    public class CategoryInCompetitionRepository : ICategoryInCompetitionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public CategoryInCompetitionRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(CategoryInCompetition entity)
        {
            _applicationContext.CategoryInCompetitions.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(CategoryInCompetition entity)
        {
            _applicationContext.CategoryInCompetitions.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public CategoryInCompetition Get(int id)
        {
            return _applicationContext.CategoryInCompetitions.Find(id);
        }

        public IEnumerable<CategoryInCompetition> GetAll()
        {
            return _applicationContext.CategoryInCompetitions.ToList();
        }

        public IEnumerable<CategoryInCompetition> GetByCompetition(Competition competition)
        {
            return _applicationContext.CategoryInCompetitions.
                Where(c=> c.CompetitionId == competition.Id)
                .ToList();
        }

        public IEnumerable<CategoryInCompetition> GetAviableCategories(byte gender,
            float weight, DateOnly birthDate, Competition competition)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            return _applicationContext.CategoryInCompetitions
                .Include(c => c.Category)
                .Where(c => c.CompetitionId == competition.Id)
                .Where(c => c.Category.Gender == gender)
                .Where(c => c.Category.MaxAge >= age && c.Category.MinAge <= age)
                .Where(c => (float)c.Category.MaxWeight >= (float)weight - (float)competition.WeightTolerance/1000)
                .ToList();
        }
    }
}
