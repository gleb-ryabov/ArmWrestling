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
    }
}
