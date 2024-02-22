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

        public int CategoryInCompetitionId { get; set; }
        public CategoryInCompetition CategoryInCompetition { get; set; }

        [ForeignKey(nameof(Person))]
        public int WinnerId { get; set; }
        public Person Winner { get; set; }

        [ForeignKey(nameof(Person))]
        public int LooserId { get; set; }
        public Person Looser { get; set; }

        public char Arm {  get; set; }

        public Duel() { }
        public void Create(CategoryInCompetition categoryInCompetition, Person winnerPerson,
            Person looserPerson, char arm)
        {
            CategoryInCompetition = categoryInCompetition;
            Winner = winnerPerson;
            Looser = looserPerson;
            Arm = arm;
        }
    }
}
