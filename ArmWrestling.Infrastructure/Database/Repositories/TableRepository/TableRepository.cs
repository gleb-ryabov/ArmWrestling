using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.TableRepository
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationContext _applicationContext;
        public TableRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public bool Create(Table entity)
        {
            _applicationContext.Tables.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(Table entity)
        {
            _applicationContext.Tables.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public Table Get(int id)
        {
            return _applicationContext.Tables.Find(id);
        }

        public IEnumerable<Table> GetAll()
        {
            return _applicationContext.Tables.ToList();
        }

        public IEnumerable<Table> GetByCompetition(Competition competition)
        {
            return _applicationContext.Tables
                .Where(t => t.CompetitionId == competition.Id)
                .ToList();
        }

        //Function for get free tables
        public IEnumerable<Table> GetFreeTables(Competition competition)
        {
            return _applicationContext.Tables
                .Where(t => t.IsBusy == 0 && t.CompetitionId == competition.Id)
                .ToList();
        }

        //Function for set category in table
        public bool SetCategory(Table table, CategoryInCompetition categoryInCompetition)
        {
            table.CategoryInCompetition = categoryInCompetition;
            table.IsBusy = 1;

            return _applicationContext.SaveChanges() > 0;
        }

        //Function for getting CategoryInCompetition in which use tables
        public IEnumerable<CategoryInCompetition> GetUsedCategories(Competition competition)
        {
            return _applicationContext.Tables
                .Select(t => t.CategoryInCompetition)
                .Where(t => t.CompetitionId == competition.Id)
                .Distinct()
                .ToList();
        }
    }
}
