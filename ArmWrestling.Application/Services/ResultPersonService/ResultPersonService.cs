using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.ResultPersonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Applications.Services.ResultPersonService
{
    public class ResultPersonService : IResultPersonService
    {
        private readonly IResultPersonRepository _resultPersonRepository;

        public ResultPersonService(IResultPersonRepository resultPersonRepository)
        {
            _resultPersonRepository = resultPersonRepository;
        }

        public ResultPerson Create(CategoryInCompetition categoryInCompetition, Person person, 
            int place, int reasonAward)
        {
            ResultPerson resultPerson = new ResultPerson()
            {
                CategoryInCompetition = categoryInCompetition,
                Person = person,
                Place = place,
                ReasonAward = reasonAward
            };
            if (_resultPersonRepository.Create(resultPerson))
                return resultPerson;
            else
                return null;
        }
    }
}
