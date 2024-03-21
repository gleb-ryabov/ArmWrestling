using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.CategoryInCompetitionRepository
{
    public interface ICategoryInCompetitionRepository : IBaseRepository<CategoryInCompetition>
    {
        IEnumerable<CategoryInCompetition> GetByCompetition(Competition competition);
        IEnumerable<CategoryInCompetition> GetAviableCategories(byte gender,
            float weight, DateOnly birthDate, Competition competition);
        bool SetComplited(int categoryInCompetitionId);
        IEnumerable<CategoryInCompetition> GetComplitedCategory(Competition competition);
    }
}
