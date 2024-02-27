using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class ResultTeam
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int Place { get; set; }
    }
}
