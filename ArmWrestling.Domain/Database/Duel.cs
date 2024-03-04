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

        public int TourNumber { get; set; }

        public char? Group { get; set; }

        /*
         * The field for identify type duel
         * 
         * 0 - The ordinary duel
         * 1 - The Superfinal
         * 2 - The Final
         * 3 - The Semi–final
         * 4 - The fight to reach the semifinals
         * 5 - The fight to reach the final
         * 6 - The fight for the 5th place
        */
        public byte? TypeDuel { get; set; }
    }
}
