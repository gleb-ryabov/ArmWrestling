using ArmWrestling.Domain.Database;
using ArmWrestling.Infrastructure.Database.Repositories.DuelRepository;
using ArmWrestling.Infrastructure.Database.Repositories.PersonRepository;
using SQLitePCL;
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
        private readonly IDuelRepository _duelRepository;

        public PersonService(IPersonRepository personRepository,
            IDuelRepository duelRepository)
        {
            _personRepository = personRepository;
            _duelRepository = duelRepository;
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
                CategoryInCompetitionId = category.Id,
                Team = team
            };
            if (_personRepository.Create(person))
                return person;
            else
                return null;
        }

        //Function for getting persons from the Group A
        public List<Person> GetPersonsInGroupA(CategoryInCompetition category, char arm)
        {
            //get persons by category (argument)
            IEnumerable <Person> persons = _personRepository.GetPersonsByCategory(category);

            //find persons, who don't have defeats
            List <Person> personsInGroupA = new List<Person>();
            foreach (Person person in persons)
            {
                if (_duelRepository.GetCountLossesByPerson(arm, person) == 0)
                    personsInGroupA.Add(person);
            }

            return personsInGroupA;
        }

        //Function for getting persons from the Group B
        public List<Person> GetPersonsInGroupB(CategoryInCompetition category, char arm)
        {
            //get persons by category (argument)
            IEnumerable<Person> persons = _personRepository.GetPersonsByCategory(category);

            //find persons, who have 1 defeat
            List<Person> personsInGroupB = new List<Person>();
            foreach (Person person in persons)
            {
                if (_duelRepository.GetCountLossesByPerson(arm, person) == 1)
                    personsInGroupB.Add(person);
            }

            return personsInGroupB;
        }

        //function for getting non dropped out persons
        public List<Person> GetNonDroppedOutPersons(CategoryInCompetition category, char arm)
        {
            //get persons from categories A and B
            List <Person> personsInGroupA = GetPersonsInGroupA(category, arm);
            List <Person> personsInGroupB = GetPersonsInGroupB(category, arm);

            //concat persons category A with persons category B
            List<Person> nonDroppedOutPersons = personsInGroupA.Concat(personsInGroupB).ToList();

            return nonDroppedOutPersons;
        }


        //Function for Draw - Get Person in Free Circle
        //if list haven't person in free circle, return null
        private Person GetPersonInFreeCircle(CategoryInCompetition category,
                        char arm, int tour, List<Person> persons)
        {
            foreach (Person person in persons)
            {
                if (!_duelRepository.CheckPersonForFreeCircle(arm, category, person, tour - 1))
                    return person;
            }

            return null;
        }

        //Function for Draw - Create Random Draw (for 1st tour)
        private List<List<Person>> GetRandomDraw(List<Person> persons)
        {
            int countPerson = persons.Count;
            List<List<Person>> draw = new List<List<Person>>();

            //random order of numbers
            Random rand = new Random();
            List<int> order = new List<int>();
            while (order.Count < countPerson)
            {
                int randomNumber = rand.Next(countPerson);

                if (!order.Contains(randomNumber))
                    order.Add(randomNumber);
            }

            //drawing up a structured list of lots
            List<Person> duel = new List<Person>();
            foreach (int number in order)
            {
                duel.Add(persons[number]);

                if (duel.Count == 2 || number == order[order.Count - 1])
                {
                    draw.Add(duel);
                    duel = new List<Person>();
                }
            }

            return draw;
        }

        //Function for draw - Find the best opponent
        private Person FindOpponent(char arm, Person person, List<Person> opponents)
        {
            int minCountDuels = 1000;
            Person goodOpponent = new Person();

            //find opponent with whom there were the least duels
            foreach (Person opponent in opponents)
            {
                if (opponent != person)
                {
                    int countDuels = _duelRepository.GetCountDuelsBetweenPersons(arm, person, opponent);

                    if (countDuels < minCountDuels)
                    {
                        goodOpponent = opponent;
                    }
                }
            }

            return goodOpponent;
        }

        //Function for draw - Create list with the best opponents
        private List<Person> CreateListWithBestOpponents(char arm, 
            List<Person> sortedPersons, List<Person> personsForAdd)
        {
            while (personsForAdd.Count > 0)
            {
                //check: is this the first person in the pair or the second
                if (sortedPersons.Count % 2 == 0)
                {
                    //if this is first person in the pair - just add the first person from the list
                    Person person = personsForAdd[0];

                    sortedPersons.Add(person);
                    personsForAdd.Remove(person);
                }
                else
                {
                    //if this is second person in the pair - get first person and find the best opponent
                    Person opponent = sortedPersons.Last();
                    Person person = FindOpponent(arm, opponent, personsForAdd);

                    sortedPersons.Add(person);
                    personsForAdd.Remove(person);
                }
            }

            return sortedPersons;
        }

        //Function for draw - sorting persons list according to the rules Armwrestling
        private List<Person> SortPersons(CategoryInCompetition category,
                        char arm, int tour, List<Person> persons)
        {
            //УДаЛИТЬ
            int firstCount = persons.Count();

            List<Person> sortedPersons = new List<Person>();

            //insert person in free circle
            Person personInFreeCircle = GetPersonInFreeCircle(category, arm, tour, persons);
            if (personInFreeCircle != null)
            {
                sortedPersons.Add(personInFreeCircle);
                persons.Remove(personInFreeCircle);
            }

            //check group(A or B). If Person have a loss, this is group B, else - A.
            int countLoss = _duelRepository.GetCountLossesByPerson(arm, persons[0]);

            //for group B
            if(countLoss != 0)
            {
                //find persons, who were eliminated from Group A in the last round
                List<Person> personsFromAInLastTour = new List<Person>();
                foreach (Person person in persons)
                {
                    bool eliminatedFromA = _duelRepository.CheckDefeatInLastRound(arm, tour, person);

                    if (eliminatedFromA)
                    {
                        personsFromAInLastTour.Add(person);
                        persons.Remove(person);
                    }
                }

                //find the best opponents and insert persons, who were eliminated from Group A in the last round
                sortedPersons = CreateListWithBestOpponents(arm, sortedPersons, personsFromAInLastTour);
            }
            //for all groups - insert other person and find the best opponents
            sortedPersons = CreateListWithBestOpponents(arm, sortedPersons, persons);

            return sortedPersons;
        }

        //Function for draw - add person pairs from sorted list in draw
        private List<List<Person>> AddPairsInDraw(List<Person> persons, List<List<Person>> draw)
        {
            //while list have person pairs
            while (persons.Count > 1)
            {
                List<Person> pairPersons = new List<Person>();

                pairPersons.Add(persons[0]);
                pairPersons.Add(persons[1]);

                draw.Add(pairPersons);

                persons.Remove(persons[0]);
                persons.Remove(persons[0]);
            }

            //if there is only one person left in list
            if (persons.Count == 1)
            {
                List<Person> freePerson = new List<Person>();

                freePerson.Add(persons[0]);
                persons.Remove(persons[0]);

                draw.Add(freePerson);

            }

            return draw;
        }

        //main function for create draw
        public List<List<Person>> CreateDraw(CategoryInCompetition category, char arm)
        {
            List<Person> persons = GetNonDroppedOutPersons(category, arm);

            List<List<Person>> draw = new List<List<Person>>();

            int countPerson = persons.Count;
            if (countPerson > 2)
            {
                //get number tour. If this is first tour - just create a random list. Else sort participants.
                int tour = _duelRepository.GetLastNumberTour(arm, category) + 1;
                if (tour > 1)
                {
                    //get persons in groups
                    List<Person> personsInGroupA = GetPersonsInGroupA(category, arm);
                    List<Person> personsInGroupB = GetPersonsInGroupB(category, arm);

                    //for group A - add person pairs in queue
                    List<Person> sortedPersonsInGroupA = SortPersons(category, arm, tour, personsInGroupA);
                    draw = AddPairsInDraw(sortedPersonsInGroupA, draw);

                    //for group B - add person pairs in queue
                    List<Person> sortedPersonsInGroupB = SortPersons(category, arm, tour, personsInGroupB);
                    draw = AddPairsInDraw(sortedPersonsInGroupB, draw);
                }
                else
                {
                    draw = GetRandomDraw(persons);
                }

            }
            //if there are only 2 persons, just add them to the queue
            else if (countPerson == 2)
                draw.Add(persons);

            return draw;
        }


    }
}
