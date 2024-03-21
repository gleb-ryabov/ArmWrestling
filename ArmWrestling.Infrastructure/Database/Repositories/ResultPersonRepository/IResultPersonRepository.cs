using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.ResultPersonRepository
{
    public interface IResultPersonRepository : IBaseRepository<ResultPerson>
    {
        List<ResultPerson> GetResultByCategory(CategoryInCompetition categoryInCompetition);
        bool ExistResult(CategoryInCompetition categoryInCompetition);
    }
}
