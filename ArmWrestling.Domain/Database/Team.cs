using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmWrestling.Domain.Database
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int Score { get; set; } = 0;
    }
}
