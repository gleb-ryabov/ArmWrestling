using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Duel
    {
        public int Id { get; set; }

        public int CompetitionId {  get; set; }
        public Competition Competition { get; set; }

        public int CategoryInCompetitionId { get; set; }
        public CategoryInCompetition CategoryInCompetition { get; set; }

        [ForeignKey(nameof(Person))]
        public int WinnerId { get; set; }
        public Person WinnerPerson { get; set; }

        [ForeignKey(nameof(Person))]
        public int LooserId { get; set; }
        public Person LooserPerson { get; set; }

        public char Arm {  get; set; }

        //public Duel(int competitionId, Competition competition, Person winnerPerson, 
        //    Person looserPerson, char arm)
        //{
        //    CompetitionId = competitionId;
        //    Competition = competition;
        //    WinnerPerson = winnerPerson;
        //    LooserPerson = looserPerson;
        //    Arm = arm;
        //}
    }
}
