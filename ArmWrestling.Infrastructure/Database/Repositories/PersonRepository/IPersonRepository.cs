using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.PersonRepository
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        bool AddScore(Person person, int score);
        IEnumerable<Person> GetPersonsByCategory(CategoryInCompetition category);
        int GetPersonCountByCategory(CategoryInCompetition category);
        IEnumerable<Person> GetPersonsSorteredByScore(CategoryInCompetition category);
        IEnumerable<Person> GetPersonsWithScore0(CategoryInCompetition category);
        Person CheckPersonIdByScore0(int personId);
    }
}
