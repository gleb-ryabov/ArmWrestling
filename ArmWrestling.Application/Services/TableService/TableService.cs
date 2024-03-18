using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.TableRepository;
using ArmWrestling.Infrastructure.Database.Repositories.TeamRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.TableService
{
    public class TableService: ITableService
    {
        private readonly ITableRepository _tableRepository;
        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public Table Create(int number, Competition competition)
        {
            int row;
            int column;

            if(number%2 == 0)
            {
                row = number / 2 - 1;
                column = 1;
            }
            else
            {
                row = number / 2;
                column = 0;
            }


            Table table = new Table()
            {
                Number = number,
                CompetitionId = competition.Id,
                IsBusy = 0
            };

            if (_tableRepository.Create(table))
                return table;
            else
                return null;
        }
    }
}
