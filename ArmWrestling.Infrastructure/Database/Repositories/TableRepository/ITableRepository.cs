using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.TableRepository
{
    public interface ITableRepository : IBaseRepository<Table>
    {
        IEnumerable<Table> GetFreeTables(Competition competition);
        bool SetCategory(Table table, CategoryInCompetition categoryInCompetition);
        IEnumerable<CategoryInCompetition> GetUsedCategories(Competition competition);
        IEnumerable<Table> GetByCompetition(Competition competition);
        Table ClearTable(Table table);
    }
}
