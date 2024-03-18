using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.TableService
{
    public interface ITableService
    {
        Table Create(int number, Competition competition);
    }
}
