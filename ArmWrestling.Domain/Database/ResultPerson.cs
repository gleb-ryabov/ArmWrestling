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

        /*
         * The procedure for determining the winner
         * 1. The amount of points
         * 2. A higher place on one of the arms
         * 3. Lower starting weight
         * 4. Less weight when re-weighing or the absence of an opponent
         * 5. Fewer defeats before reaching the finals
         * 6. Victory in an additional decisive match
         * 7. 3rd place on both hands, or 2nd and 3rd place on each hand
         */
        public int ReasonAward { get; set; }
    }
}
