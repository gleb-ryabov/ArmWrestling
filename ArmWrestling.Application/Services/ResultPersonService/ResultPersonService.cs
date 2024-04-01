using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using ArmWrestling.Infrastructure.Database.Repositories.ResultPersonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArmWrestling.Applications.Services.ResultPersonService
{
    public class ResultPersonService : IResultPersonService
    {
        private readonly IResultPersonRepository _resultPersonRepository;
        private readonly IDuelRepository _duelRepository;
        private readonly IPersonRepository _personRepository;

        public ResultPersonService(IResultPersonRepository resultPersonRepository,
            IDuelRepository duelRepository,
            IPersonRepository personRepository)
        {
            _duelRepository = duelRepository;
            _personRepository = personRepository;
            _resultPersonRepository = resultPersonRepository;
        }

        public ResultPerson Create(CategoryInCompetition categoryInCompetition, Person person, 
            int place, int reasonAward)
        {
            ResultPerson resultPerson = new ResultPerson()
            {
                CategoryInCompetitionId = categoryInCompetition.Id,
                PersonId = person.Id,
                Place = place,
                ReasonAward = reasonAward
            };
            if (_resultPersonRepository.Create(resultPerson))
                return resultPerson;
            else
                return null;
        }

        //Function for get result for category in competition
        public List<ResultPerson> GetResult(CategoryInCompetition categoryInCompetition)
        {
            //check exist result
            bool existResult = _resultPersonRepository.ExistResult(categoryInCompetition);
            if (!existResult)
                SetResult(categoryInCompetition);

            List<ResultPerson> results = _resultPersonRepository.GetResultByCategory(categoryInCompetition);

            //set person on result
            foreach (ResultPerson result in results)
            {
                if (result.Person == null)
                    result.Person = _personRepository.Get(result.PersonId);
            }

            return results;
        }

        //Function for set result for category in competition
        public void SetResult(CategoryInCompetition categoryInCompetition)
        {
            //checking that competition for this category complited
            if (categoryInCompetition.Complited == 1)
            {
                //lists of sorted person from the best to the worse place
                List<Person> sortedPersonInLeftArm = GetPersonByPlaceInArm('л');
                List<Person> sortedPersonInRightArm = GetPersonByPlaceInArm('п');

                //scoring points to persons
                AddScore(sortedPersonInLeftArm);
                AddScore(sortedPersonInRightArm);

                //calculation of places
                List<Person> personWithScore = _personRepository.GetPersonsSorteredByScore(categoryInCompetition).ToList();
                personWithScore = SortPersonByWeight(personWithScore);
                List<Person> personsWithoutScore = SortPersonsWithScore0(sortedPersonInLeftArm, sortedPersonInRightArm);
                //List<Person> finalPersonList = personWithScore;
                List<Person> finalPersonList = personWithScore.Concat(personsWithoutScore).ToList();

                //set place
                SetPlace(finalPersonList);

            }

            //function for getting a list of person sorted by the places occupied in the hand
            List<Person> GetPersonByPlaceInArm(char arm)
            {
                //list for sorted persons by place
                List<Person> sortedPerson = new List<Person>();

                int countTours = _duelRepository.GetLastNumberTour(arm, categoryInCompetition);

                for (int tourNumber = 1; tourNumber < countTours + 1; tourNumber++)
                {
                    //find losers in tour
                    List<Person> loosersInTour = _duelRepository.GetLoosersInTour
                        (categoryInCompetition, arm, tourNumber).ToList();

                    //find losers in tour who have a 2 defeats
                    List<Person> loosersWithTwoDefeats = new List<Person>();
                    foreach(Person person in loosersInTour)
                    {
                        if(person is not null)
                        {
                            int countLosses = _duelRepository.GetCountLossesByPersonInTour(arm, tourNumber, person);
                            if (countLosses == 2)
                                loosersWithTwoDefeats.Add(person);
                        }
                    }

                    //sorting person descending order by weight
                    if (loosersWithTwoDefeats is not null && loosersWithTwoDefeats.Count > 0)
                    {
                        List<Person> sotredPersonInTour = new List<Person>();
                        sotredPersonInTour = loosersWithTwoDefeats.OrderByDescending(p => p.Weight).ToList();

                        foreach (Person person in sotredPersonInTour)
                            sortedPerson.Add(person);
                    }
   
                }

                //add winner in list
                int winnerId = _duelRepository.GetWinnerIdInTour(categoryInCompetition, arm, countTours);
                Person winner = _personRepository.Get(winnerId);
                sortedPerson.Add(winner);

                //reverse list
                sortedPerson.Reverse();

                return sortedPerson;
            }

            //function for sort person who have score equal 0 by place
            List<Person> SortPersonsWithScore0(List<Person> personsInLeftArm, List<Person> personsInRightArm)
            {
                //get lists with persons who have score equal 0
                List<Person> personsInLeftArmWithScore0 = SelectPersonsWithScore0(personsInLeftArm);
                List<Person> personsInRightArmWithScore0 = SelectPersonsWithScore0(personsInRightArm);

                //dictionary for persons score
                Dictionary<Person, int> personsWithScore = new Dictionary<Person, int>();

                //add scores in dictionary for each person
                foreach(Person person in personsInLeftArmWithScore0)
                {
                    int indexInLeftArm = personsInLeftArmWithScore0.IndexOf(person);
                    int indexInRightArm = personsInRightArmWithScore0.IndexOf(person);
                    int sumIndex = indexInLeftArm + indexInRightArm;

                    personsWithScore.Add(person, sumIndex);
                }

                //sorting persons descending by score
                List<Person> sortedPersons = personsWithScore.OrderByDescending(p => p.Value)
                                                                .Select(p => p.Key)
                                                                .ToList();
                //sorting persons by weiht if their weight is equal
                sortedPersons = SortPersonByWeight(sortedPersons);

                //function for select persons with score equal 0 by list with result person in one arm
                List<Person> SelectPersonsWithScore0(List<Person> persons)
                {
                    List<Person> selectedPersons = new List<Person>();
                    foreach (Person person in persons)
                    {
                        //checking person by score. If score equal 0, add in list.
                        Person personWithScore0 = _personRepository.CheckPersonIdByScore0(person.Id);
                        if (personWithScore0 != null)
                            selectedPersons.Add(personWithScore0);
                    }
                    return selectedPersons;
                }

                return sortedPersons;
            }

            //function for add score by list
            void AddScore (List<Person> persons)
            {
                foreach(Person person in persons)
                {
                    int place = persons.IndexOf(person) + 1;
                    int score = 0;

                    switch (place)
                    {
                        case 1:
                            score = 25;
                            break;
                        case 2:
                            score = 17; 
                            break;
                        case 3:
                            score = 9;
                            break;
                        case 4:
                            score = 5;
                            break;
                        case 5:
                            score = 3;
                            break;
                        case 6:
                            score = 2;
                            break;
                    }

                    _personRepository.AddScore(person, score);
                }
            }

            //function to sort people by weight if their scores are equal
            List<Person> SortPersonByWeight(List<Person> Persons)
            {
                for (int i = 0;  i < Persons.Count - 1; i++)
                {
                    Person firstPerson = Persons[i];
                    Person secondPerson = Persons[i+1];

                    if(firstPerson.Score == secondPerson.Score)
                    {
                        if(firstPerson.Weight > secondPerson.Weight)
                        {
                            Person temp = firstPerson;
                            Persons[i] = secondPerson;
                            Persons[i+1] = temp;
                        }

                    }
                }

                return Persons;
            }

            //function for set place for person
            void SetPlace(List<Person> persons)
            {
                for(int place = 1; place < persons.Count + 1; place++)
                {
                    Person person = persons[place-1];
                    Create(categoryInCompetition, person, place, 1);
                }
            }
        }
    }
}
