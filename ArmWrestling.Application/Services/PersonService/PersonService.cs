using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArmWrestling.Applications.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public Person Create(string surname, string name, string middleName, byte gender, DateOnly birthDate,
            float weight, CategoryInCompetition category, Team team = null)
        {
            Person person = new Person()
            {
                Surname = surname,
                Name = name,
                MiddleName = middleName,
                Gender = gender,
                BirthDate = birthDate,
                Weight = weight,
                CategoryInCompetition = category,
                Team = team
            };
            if (_personRepository.Create(person))
                return person;
            else
                return null;
        }
    }
}
