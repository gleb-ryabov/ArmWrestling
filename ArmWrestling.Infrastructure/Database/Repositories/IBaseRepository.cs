using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories
{
    public interface IBaseRepository <T>
    {
        bool Create(T entity);
        bool Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
