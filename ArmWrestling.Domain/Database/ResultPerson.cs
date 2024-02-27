using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class ResultPerson
    {
        public int Id { get; set; }

        public int CategoryInCompetitionId { get; set; }
        public CategoryInCompetition CategoryInCompetition { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int Place {  get; set; }
    }
}
