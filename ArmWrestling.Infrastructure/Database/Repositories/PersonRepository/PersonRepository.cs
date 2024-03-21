using ArmWrestling.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Infrastructure.Database.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PersonRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Create(Person entity)
        {
            _applicationContext.Persons.Add(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public bool Delete(Person entity)
        {
            _applicationContext.Persons.Remove(entity);

            return _applicationContext.SaveChanges() > 0;
        }

        public Person Get(int id)
        {
            return _applicationContext.Persons.Find(id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _applicationContext.Persons.ToList();
        }

        public bool AddScore(Person person, int score)
        {
            Person modifiedPerson = Get(person.Id);
            modifiedPerson.Score += score;
            return _applicationContext.SaveChanges() > 0;
        }

        public IEnumerable<Person> GetPersonsByCategory (CategoryInCompetition category)
        {
            return _applicationContext.Persons
                .Where(p=> p.CategoryInCompetition == category)
                .OrderBy(p=> p.Surname)
                .ToList();
        }
        
        public int GetPersonCountByCategory (CategoryInCompetition category)
        {
            return _applicationContext.Persons
                .Where(p => p.CategoryInCompetitionId == category.Id)
                .Count();
        }


        //Function for getting persons, sortered by score (if score > 0)
        public IEnumerable<Person> GetPersonsSorteredByScore(CategoryInCompetition category)
        {
            return _applicationContext.Persons
                .Where(p => p.CategoryInCompetitionId == category.Id)
                .Where(p => p.Score > 0)
                .OrderByDescending(p => p.Score)
                .ToList();
        }

        //Function for getting persons, who have score = 0
        public IEnumerable<Person> GetPersonsWithScore0(CategoryInCompetition category)
        {
            return _applicationContext.Persons
                .Where(p => p.CategoryInCompetitionId == category.Id)
                .Where(p => p.Score == 0)
                .OrderBy(p => p.Score)
                .ToList();
        }
    }
}
